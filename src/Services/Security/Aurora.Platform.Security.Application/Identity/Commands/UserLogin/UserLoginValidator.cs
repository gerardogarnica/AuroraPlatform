using FluentValidation;

namespace Aurora.Platform.Security.Application.Identity.Commands.UserLogin
{
    public class UserLoginValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Login name is required.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}