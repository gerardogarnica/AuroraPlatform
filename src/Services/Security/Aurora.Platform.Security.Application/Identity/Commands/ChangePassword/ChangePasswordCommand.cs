using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}