using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery : IRequest<PagedCollection<UserInfo>>
    {
        public PagedViewRequest PagedViewRequest { get; init; }
        public int RoleId { get; init; }
        public bool OnlyActives { get; init; }
    }
}