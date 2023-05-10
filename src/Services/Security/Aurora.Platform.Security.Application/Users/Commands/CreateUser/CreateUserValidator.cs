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

            RuleFor(p => p.LoginName)
                .NotEmpty().WithMessage("Login name is required.")
                .MinimumLength(5).WithMessage("The minimum login name length is 5 characters.")
                .MaximumLength(35).WithMessage("The maximum login name length is 35 characters.")
                .MustAsync(LoginNameIsNotAvailableAsync).WithMessage("{PropertyName} already exists and cannot be created again.")
                .Must(EmailIsNotValid).WithMessage("{PropertyName} does not have a valid email.");

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

        private async Task<bool> LoginNameIsNotAvailableAsync(string loginName, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAsync(loginName) == null;
        }

        private bool EmailIsNotValid(string email)
        {
            return RegexUtils.IsValidEmail(email);
        }
    }
}