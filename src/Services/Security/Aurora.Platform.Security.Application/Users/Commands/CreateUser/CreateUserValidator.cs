using Aurora.Framework.Utils;
using Aurora.Platform.Security.Domain.Repositories;
using FluentValidation;

namespace Aurora.Platform.Security.Application.Users.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(50).WithMessage("The maximum email length is 50 characters.")
                .MustAsync(EmailIsNotAvailableAsync).WithMessage("{PropertyName} already exists and cannot be created again.")
                .Must(EmailIsNotValid).WithMessage("{PropertyName} does not have a valid email.");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(40).WithMessage("The maximum first name length is 40 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(40).WithMessage("The maximum last name length is 40 characters.");
        }

        private async Task<bool> EmailIsNotAvailableAsync(string email, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAsync(email) == null;
        }

        private bool EmailIsNotValid(string email)
        {
            return RegexUtils.IsValidEmail(email);
        }
    }
}