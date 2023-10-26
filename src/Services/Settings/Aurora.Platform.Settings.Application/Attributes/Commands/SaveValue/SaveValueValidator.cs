using FluentValidation;

namespace Aurora.Platform.Settings.Application.Attributes.Commands.SaveValue
{
    public class SaveValueValidator : AbstractValidator<SaveValueCommand>
    {
        public SaveValueValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MinimumLength(3).WithMessage("The minimum code length is 3 characters.")
                .MaximumLength(40).WithMessage("The maximum code length is 40 characters.");

            RuleFor(p => p.Notes)
                .MaximumLength(2000).WithMessage("The maximum notes length is 2000 characters.");
        }
    }
}