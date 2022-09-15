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