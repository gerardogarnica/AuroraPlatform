using Aurora.Framework.Security;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Queries.GetUserByLogin
{
    public record GetUserByLoginQuery : IRequest<UserInfo>
    {
        public string LoginName { get; init; }
    }
}