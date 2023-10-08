using FluentValidation;

namespace Aurora.Platform.Security.Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("The maximum name length is 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(100).WithMessage("The maximum description length is 100 characters.");

            RuleFor(p => p.Notes)
                .MaximumLength(2000).WithMessage("The maximum notes length is 2000 characters.");
        }
    }
}