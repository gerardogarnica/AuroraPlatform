using FluentValidation;

namespace Aurora.Platform.Security.Application.Roles.Commands.CreateRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(p => p.Application)
                .NotEmpty().WithMessage("Application is required.")
                .MaximumLength(50).WithMessage("The maximum application length is 50 characters.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("The maximum name length is 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(400).WithMessage("The maximum description length is 400 characters.");
        }
    }
}