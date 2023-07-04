using FluentValidation;

namespace Aurora.Platform.Security.Application.Roles.Commands.UpdateRole
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(400).WithMessage("The maximum description length is 400 characters.");
        }
    }
}