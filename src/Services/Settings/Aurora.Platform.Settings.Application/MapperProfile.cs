using Aurora.Framework;
using Aurora.Framework.Entities;
using Aurora.Framework.Settings;
using Aurora.Platform.Settings.Application.Attributes;
using Aurora.Platform.Settings.Application.Options;
using Aurora.Platform.Settings.Application.Options.Commands.CreateOption;
using Aurora.Platform.Settings.Domain.Entities;
using AutoMapper;
using OptionsCatalogItemEntity = Aurora.Platform.Settings.Domain.Entities.OptionsCatalogItem;
using OptionsCatalogItemModel = Aurora.Platform.Settings.Application.Options.OptionsCatalogItem;

namespace Aurora.Platform.Settings.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Source: entity. Destination: view model.
            CreateMap<AttributeSetting, AttributeSettingModel>()
                .ForMember(d => d.AttributeId, o => o.MapFrom(o => o.Id))
                .ForMember(d => d.BooleanSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Boolean.ToString() ? new BooleanAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.IntegerSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Integer.ToString() ? new IntegerAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.MoneySetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Money.ToString() ? new MoneyAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.NumericSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Numeric.ToString() ? new NumericAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.OptionsSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Options.ToString() ? new OptionsAttributeSetting(o.Configuration) : null))
                .ForMember(d => d.TextSetting, o => o.MapFrom(o => o.DataType == AuroraDataType.Text.ToString() ? new TextAttributeSetting(o.Configuration) : null));

            CreateMap<PagedCollection<AttributeSetting>, PagedCollection<AttributeSettingModel>>();

            CreateMap<AttributeValue, AttributeValueModel>()
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

            CreateMap<OptionsCatalog, OptionsCatalogModel>().ForMember(d => d.OptionsId, o => o.MapFrom(o => o.Id));
            CreateMap<OptionsCatalogItemEntity, OptionsCatalogItemModel>().ForMember(d => d.ItemId, o => o.MapFrom(o => o.Id));

            // Source: command. Destination: entity.
            CreateMap<CreateOptionCommand, OptionsCatalog>()
                .ForMember(d => d.Code, o => o.MapFrom(o => o.Code != null ? o.Code.Trim() : string.Empty))
                .ForMember(d => d.Name, o => o.MapFrom(o => o.Name != null ? o.Name.Trim() : string.Empty))
                .ForMember(d => d.Description, o => o.MapFrom(o => o.Description != null ? o.Description.Trim() : string.Empty));

            CreateMap<CreateOptionItem, OptionsCatalogItemEntity>();
        }
    }
}