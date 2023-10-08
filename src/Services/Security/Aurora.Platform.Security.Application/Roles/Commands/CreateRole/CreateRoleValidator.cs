using FluentValidation;

namespace Aurora.Platform.Security.Application.Roles.Commands.CreateRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("The maximum name length is 50 characters.");

            RuleFor(p => p.AppCode)
                .NotEmpty().WithMessage("Application code is required.")
                .MaximumLength(50).WithMessage("The maximum application code length is 50 characters.");

            RuleFor(p => p.AppName)
                .NotEmpty().WithMessage("Application name is required.")
                .MaximumLength(50).WithMessage("The maximum application name length is 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(100).WithMessage("The maximum description length is 100 characters.");

            RuleFor(p => p.Notes)
                .MaximumLength(2000).WithMessage("The maximum notes length is 2000 characters.");
        }
    }
}