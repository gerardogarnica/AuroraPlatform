using FluentValidation;

namespace Aurora.Platform.Settings.Application.Options.Commands.CreateOption
{
    public class CreateOptionValidator : AbstractValidator<CreateOptionCommand>
    {
        public CreateOptionValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MinimumLength(3).WithMessage("The minimum code length is 3 characters.")
                .MaximumLength(40).WithMessage("The maximum code length is 40 characters.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("The minimum name length is 3 characters.")
                .MaximumLength(50).WithMessage("The maximum name length is 50 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(100).WithMessage("The maximum description length is 100 characters.");

            RuleFor(p => p.Items)
                .Must(p => p.Any()).WithMessage("The option must have at least one element.");
        }
    }
}