using FluentValidation;

namespace Aurora.Platform.Settings.Application.Options.Commands.UpdateOption
{
    public class UpdateOptionValidator : AbstractValidator<UpdateOptionCommand>
    {
        public UpdateOptionValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("The minimum name length is 3 characters.")
                .MaximumLength(50).WithMessage("The maximum name length is 50 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(100).WithMessage("The maximum description length is 100 characters.");
        }
    }
}