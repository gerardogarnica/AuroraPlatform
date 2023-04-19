using Aurora.Platform.Security.Domain.Entities;
using MediatR;

namespace Aurora.Platform.Security.Application.UserLogin
{
    public class UserLoginCommand : IRequest<IdentityAccess>
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}