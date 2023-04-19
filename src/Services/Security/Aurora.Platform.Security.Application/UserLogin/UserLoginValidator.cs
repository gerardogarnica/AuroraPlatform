using FluentValidation;

namespace Aurora.Platform.Security.Application.UserLogin
{
    public class UserLoginValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginValidator() 
        {
            RuleFor(p => p.LoginName)
                .NotEmpty()
                .WithMessage("Login name is required.");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}