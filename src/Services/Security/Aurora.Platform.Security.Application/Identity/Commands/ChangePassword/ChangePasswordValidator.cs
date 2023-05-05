using FluentValidation;

namespace Aurora.Platform.Security.Application.Identity.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(p => p.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("The minimum password length is 8 characters.")
                .MaximumLength(25).WithMessage("The maximum password length is 25 characters.");
        }
    }
}