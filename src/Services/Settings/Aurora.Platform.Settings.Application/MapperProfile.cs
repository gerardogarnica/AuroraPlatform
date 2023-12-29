using Aurora.Framework;
using Aurora.Framework.Entities;
using Aurora.Framework.Settings;
using Aurora.Platform.Settings.Application.Attributes.Commands.CreateSetting;
using Aurora.Platform.Settings.Application.Options.Commands.CreateOption;
using Aurora.Platform.Settings.Application.Options.Commands.SaveItem;
using AutoMapper;
using AttributeSettingEntity = Aurora.Platform.Settings.Domain.Entities.AttributeSetting;
using AttributeSettingModel = Aurora.Framework.Platform.Attributes.AttributeSetting;
using AttributeValueEntity = Aurora.Platform.Settings.Domain.Entities.AttributeValue;
using AttributeValueModel = Aurora.Framework.Platform.Attributes.AttributeValue;
using OptionsCatalogEntity = Aurora.Platform.Settings.Domain.Entities.OptionsCatalog;
using OptionsCatalogItemEntity = Aurora.Platform.Settings.Domain.Entities.OptionsCatalogItem;
using OptionsCatalogItemModel = Aurora.Framework.Platform.Options.OptionsCatalogItem;
using OptionsCatalogModel = Aurora.Framework.Platform.Options.OptionsCatalog;

namespace Aurora.Platform.Settings.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Source: entity. Destination: view model.
            CreateMap<AttributeSettingEntity, AttributeSettingModel>()
                .ForMember(d => d.AttributeId, o => o.MapFrom(o => o.Id))
                .ForMember(d => d.BooleanSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Boolean.ToString() ? new BooleanAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.IntegerSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Integer.ToString() ? new IntegerAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.MoneySetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Money.ToString() ? new MoneyAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.NumericSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Numeric.ToString() ? new NumericAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.OptionsSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Options.ToString() ? new OptionsAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.TextSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Text.ToString() ? new TextAttributeSetting(o.Configuration) : null));

            CreateMap<PagedCollection<AttributeSettingEntity>, PagedCollection<AttributeSettingModel>>();

            CreateMap<AttributeValueEntity, AttributeValueModel>()
                .ForMember(d => d.AttributeId, o => o.MapFrom(o => o.Id))
                .ForMember(d => d.Code, o => o.MapFrom(o => o.AttributeSetting.Code))
                .ForMember(d => d.DataType, o => o.MapFrom(o => o.AttributeSetting.DataType))
                .ForMember(d => d.Setting, o => o.MapFrom(o => o.AttributeSetting))
                .ForMember(d => d.BooleanValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Boolean.ToString() ? new BooleanAttributeValue(o.Value) : null))
                .ForMember(d => d.IntegerValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Integer.ToString() ? new IntegerAttributeValue(o.Value) : null))
                .ForMember(d => d.MoneyValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Money.ToString() ? new MoneyAttributeValue(o.Value) : null))
                .ForMember(d => d.NumericValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Numeric.ToString() ? new NumericAttributeValue(o.Value) : null))
                .ForMember(d => d.OptionsValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Options.ToString() ? new OptionsAttributeValue(o.Value) : null))
                .ForMember(d => d.TextValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Text.ToString() ? new TextAttributeValue(o.Value) : null));

            CreateMap<OptionsCatalogEntity, OptionsCatalogModel>().ForMember(d => d.OptionsId, o => o.MapFrom(o => o.Id));
            CreateMap<OptionsCatalogItemEntity, OptionsCatalogItemModel>().ForMember(d => d.ItemId, o => o.MapFrom(o => o.Id));
            CreateMap<PagedCollection<OptionsCatalogEntity>, PagedCollection<OptionsCatalogModel>>();

            // Source: command. Destination: entity.
            CreateMap<CreateSettingCommand, AttributeSettingEntity>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code != null ? o.Code.Trim() : string.Empty))
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name != null ? o.Name.Trim() : string.Empty))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description != null ? o.Description.Trim() : string.Empty))
                .ForMember(d => d.ScopeType, o => o.MapFrom(o => o.ScopeType != null ? o.ScopeType.Trim() : string.Empty))
                .ForMember(d => d.DataType, o => o.MapFrom(o => o.DataType.ToString()))
                .ForMember(d => d.Configuration, o => o.MapFrom(o => o.GetSettingString()));

            CreateMap<CreateOptionCommand, OptionsCatalogEntity>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code != null ? o.Code.Trim() : string.Empty))
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name != null ? o.Name.Trim() : string.Empty))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description != null ? o.Description.Trim() : string.Empty));

            CreateMap<CreateOptionItem, OptionsCatalogItemEntity>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code != null ? o.Code.Trim() : string.Empty))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description != null ? o.Description.Trim() : string.Empty));

            CreateMap<SaveItemCommand, OptionsCatalogItemEntity>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.ItemCode != null ? o.ItemCode.Trim() : string.Empty))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description != null ? o.Description.Trim() : string.Empty));
        }
    }
}