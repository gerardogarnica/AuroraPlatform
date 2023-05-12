using Aurora.Framework.Utils;
using Aurora.Platform.Security.Domain.Repositories;
using FluentValidation;

namespace Aurora.Platform.Security.Application.Users.Commands.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator(IUserRepository userRepository)
        {
            RuleFor(p => p.LoginName)
                .NotEmpty().WithMessage("Login name is required.");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(40).WithMessage("The maximum first name length is 40 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(40).WithMessage("The maximum last name length is 40 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(50).WithMessage("The maximum email length is 50 characters.")
                .Must(EmailIsNotValid).WithMessage("{PropertyName} does not have a valid email.");
        }

        private bool EmailIsNotValid(string email)
        {
            return RegexUtils.IsValidEmail(email);
        }
    }
}