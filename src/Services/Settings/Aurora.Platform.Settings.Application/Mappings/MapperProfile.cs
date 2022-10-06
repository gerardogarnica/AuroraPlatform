using Aurora.Framework;
using Aurora.Framework.Entities;
using Aurora.Framework.Settings;
using Aurora.Platform.Settings.Application.Commands;
using Aurora.Platform.Settings.Application.Queries;
using Aurora.Platform.Settings.Domain.Entities;
using AutoMapper;
using OptionsListItem = Aurora.Platform.Settings.Domain.Entities.OptionsListItem;
using OptionsListItemViewModel = Aurora.Platform.Settings.Application.Queries.OptionsListItem;

namespace Aurora.Platform.Settings.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Source: entity. Destination: view model.
            CreateMap<AttributeSetting, AttributeSettingModel>()
                .ForMember(d => d.BooleanSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Boolean.ToString() ? new BooleanAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.IntegerSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Integer.ToString() ? new IntegerAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.MoneySetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Money.ToString() ? new MoneyAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.NumericSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Numeric.ToString() ? new NumericAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.OptionsListSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.OptionsList.ToString() ? new OptionsListAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.TextSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Text.ToString() ? new TextAttributeSetting(o.Configuration) : null));

            CreateMap<PagedCollection<AttributeSetting>, PagedCollection<AttributeSettingModel>>();

            CreateMap<AttributeValue, AttributeValueModel>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.AttributeSetting.Code))
                .ForMember(d => d.DataType, o => o.MapFrom(o => o.AttributeSetting.DataType))
                .ForMember(d => d.BooleanValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Boolean.ToString() ? new BooleanAttributeValue(o.Value) : null))
                .ForMember(d => d.IntegerValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Integer.ToString() ? new IntegerAttributeValue(o.Value) : null))
                .ForMember(d => d.MoneyValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Money.ToString() ? new MoneyAttributeValue(o.Value) : null))
                .ForMember(d => d.NumericValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Numeric.ToString() ? new NumericAttributeValue(o.Value) : null))
                .ForMember(d => d.OptionsListValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.OptionsList.ToString() ? new OptionsListAttributeValue(o.Value) : null))
                .ForMember(d => d.TextValue, o => o.MapFrom(o => o.AttributeSetting.DataType == AuroraDataType.Text.ToString() ? new TextAttributeValue(o.Value) : null));

            CreateMap<OptionsList, OptionsListViewModel>();
            CreateMap<OptionsListItem, OptionsListItemViewModel>();

            // Source: command. Destination: entity.
            CreateMap<CreateOptionsListCommand, OptionsList>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code != null ? o.Code.Trim() : string.Empty))
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name != null ? o.Name.Trim() : string.Empty))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description != null ? o.Description.Trim() : string.Empty));

            CreateMap<CreateOptionsListItem, OptionsListItem>();
        }
    }
}