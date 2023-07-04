using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Application.Roles.Commands.CreateRole;
using Aurora.Platform.Security.Application.Users.Commands.CreateUser;
using Aurora.Platform.Security.Domain.Entities;
using AutoMapper;

namespace Aurora.Platform.Security.Application
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Source: entity. Destination: view model.
            CreateMap<Role, RoleInfo>()
                .ForMember(d => d.RoleId, o => o.MapFrom(o => o.Id));

            CreateMap<User, UserInfo>()
                .ForMember(d => d.UserId, o => o.MapFrom(o => o.Id));

            CreateMap<PagedCollection<Role>, PagedCollection<RoleInfo>>();
            CreateMap<PagedCollection<User>, PagedCollection<UserInfo>>();

            // Source: command. Destination: entity.
            CreateMap<CreateRoleCommand, Role>();
            CreateMap<CreateUserCommand, User>();
        }
    }
}