using Aurora.Framework;
using Aurora.Framework.Entities;
using Aurora.Framework.Settings;
using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Repositories;
using AutoMapper;
using System.Linq.Expressions;

namespace Aurora.Platform.Settings.Application.Queries
{
    public interface IAttributeQueries
    {
        Task<AttributeSettingModel> GetSettingAsync(string code);
        Task<PagedCollection<AttributeSettingModel>> GetSettingListAsync(PagedViewRequest viewRequest, string scope);
        Task<AttributeValueModel> GetValueAsync(string code, int relationshipId);
        Task<IList<AttributeValueModel>> GetValueListAsync(string scopeType, int relationshipId);
    }

    public class AttributeQueries : IAttributeQueries
    {
        #region Private members

        private readonly IAttributeSettingRepository _settingRepository;
        private readonly IAttributeValueRepository _valueRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public AttributeQueries(
            IAttributeSettingRepository settingRepository,
            IAttributeValueRepository valueRepository,
            IMapper mapper)
        {
            _settingRepository = settingRepository ?? throw new ArgumentNullException(nameof(settingRepository));
            _valueRepository = valueRepository ?? throw new ArgumentNullException(nameof(valueRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IAttributeQueries implementation

        async Task<AttributeSettingModel> IAttributeQueries.GetSettingAsync(string code)
        {
            var attributeSetting = await GetExistentAttributeSetting(code);
            if (attributeSetting == null) return null;

            return _mapper.Map<AttributeSettingModel>(attributeSetting);
        }

        async Task<PagedCollection<AttributeSettingModel>> IAttributeQueries.GetSettingListAsync(PagedViewRequest viewRequest, string scope)
        {
            // Add filters
            Expression<Func<AttributeSetting, bool>> filter = x => x.Equals(x);
            if (!string.IsNullOrWhiteSpace(scope)) filter.And(x => x.ScopeType == scope);

            var attributeSettings = await _settingRepository.GetPagedListAsync(viewRequest, filter, x => x.Name);

            return _mapper.Map<PagedCollection<AttributeSettingModel>>(attributeSettings);
        }

        async Task<AttributeValueModel> IAttributeQueries.GetValueAsync(string code, int relationshipId)
        {
            var attributeValue = await _valueRepository.GetByCodeAsync(code, relationshipId);

            if (attributeValue == null)
            {
                var attributeSetting = await GetExistentAttributeSetting(code);
                if (attributeSetting == null) return null;

                var settingModel = _mapper.Map<AttributeSettingModel>(attributeSetting);
                if (settingModel == null) return null;

                attributeValue = new AttributeValue()
                {
                    Id = attributeSetting.Id,
                    RelationshipId = relationshipId,
                    Value = CreateDefaultValue(settingModel),
                    AttributeSetting = attributeSetting
                };
            }

            return _mapper.Map<AttributeValueModel>(attributeValue);
        }

        async Task<IList<AttributeValueModel>> IAttributeQueries.GetValueListAsync(string scopeType, int relationshipId)
        {
            var attributeSettings = await GetSettingList(scopeType);
            var attributeValues = await _valueRepository.GetListAsync(scopeType, relationshipId);

            foreach (var attributeSetting in attributeSettings)
            {
                if (attributeValues.ToList().Any(x => x.Id.Equals(attributeSetting.Id)))
                    continue;

                var settingModel = _mapper.Map<AttributeSettingModel>(attributeSetting);

                attributeValues.Add(new AttributeValue()
                {
                    Id = attributeSetting.Id,
                    RelationshipId = relationshipId,
                    Value = CreateDefaultValue(settingModel),
                    AttributeSetting = attributeSetting
                });
            }

            return _mapper.Map<IList<AttributeValueModel>>(attributeValues);
        }

        #endregion

        #region Private methods

        private async Task<IEnumerable<AttributeSetting>> GetSettingList(string scopeType)
        {
            return await _settingRepository.GetListAsync(x => x.ScopeType == scopeType, x => x.Name);
        }

        private async Task<AttributeSetting> GetExistentAttributeSetting(string code)
        {
            return await _settingRepository.GetAsync(x => x.Code == code);
        }

        private static string CreateDefaultValue(AttributeSettingModel setting)
        {
            switch (setting.DataType)
            {
                case AuroraDataType.Boolean:
                    var booleanValue = new BooleanAttributeValue()
                    {
                        Value = setting.BooleanSetting.DefaultValue
                    };
                    return booleanValue.GetValueWrapper();

                case AuroraDataType.Integer:
                    var integerValue = new IntegerAttributeValue()
                    {
                        Value = setting.IntegerSetting.DefaultValue
                    };
                    return integerValue.GetValueWrapper(setting.IntegerSetting);

                case AuroraDataType.Money:
                    var moneyValue = new MoneyAttributeValue()
                    {
                        Value = setting.MoneySetting.DefaultValue
                    };
                    return moneyValue.GetValueWrapper(setting.MoneySetting);

                case AuroraDataType.Numeric:
                    var numericValue = new NumericAttributeValue()
                    {
                        Value = setting.NumericSetting.DefaultValue
                    };
                    return numericValue.GetValueWrapper(setting.NumericSetting);

                case AuroraDataType.OptionsList:
                    var optionListValue = new OptionsListAttributeValue()
                    {
                        ItemCodes = setting.OptionsListSetting.DefaultItemCodes
                    };
                    return optionListValue.GetValueWrapper(setting.OptionsListSetting);

                case AuroraDataType.Text:
                    var textValue = new TextAttributeValue()
                    {
                        Value = setting.TextSetting.DefaultValue
                    };
                    return textValue.GetValueWrapper(setting.TextSetting);

                default:
                    return string.Empty;
            }
        }

        #endregion
    }
}