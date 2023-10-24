using FluentValidation;

namespace Aurora.Platform.Settings.Application.Options.Commands.CreateOption
{
    public class CreateOptionItemValidator : AbstractValidator<CreateOptionItem>
    {
        public CreateOptionItemValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MinimumLength(3).WithMessage("The minimum code length is 3 characters.")
                .MaximumLength(40).WithMessage("The maximum code length is 40 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(3).WithMessage("The minimum description length is 3 characters.")
                .MaximumLength(100).WithMessage("The maximum description length is 100 characters.");
        }
    }
}