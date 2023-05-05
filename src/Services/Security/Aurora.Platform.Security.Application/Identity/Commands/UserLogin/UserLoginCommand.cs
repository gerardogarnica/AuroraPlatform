using Aurora.Framework.Security;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.UserLogin
{
    public class UserLoginCommand : IRequest<IdentityToken>
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}