using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using AutoMapper;

namespace Aurora.Platform.Security.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Source: entity. Destination: view model.
            CreateMap<Role, RoleInfo>();
            CreateMap<User, UserInfo>()
                .ForMember(d => d.UserId, o => o.MapFrom(o => o.Id))
                .ForMember(d => d.PasswordMustChange, o => o.MapFrom(o => o.Credential.MustChange))
                .ForMember(d => d.PasswordExpirationDate, o => o.MapFrom(o => o.Credential.ExpirationDate));
        }
    }
}