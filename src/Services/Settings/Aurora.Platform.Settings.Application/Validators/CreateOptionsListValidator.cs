using Aurora.Platform.Settings.Application.Commands;
using FluentValidation;

namespace Aurora.Platform.Settings.Application.Validators
{
    public class CreateOptionsListValidator : AbstractValidator<CreateOptionsListCommand>
    {
        public CreateOptionsListValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MinimumLength(3).WithMessage("The minimum length of the code is 3 characters.")
                .MaximumLength(40).WithMessage("The maximum length of the code is 40 characters.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("The minimum length of the name is 3 characters.")
                .MaximumLength(50).WithMessage("The maximum length of the name is 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(3).WithMessage("The minimum length of the description is 3 characters.")
                .MaximumLength(200).WithMessage("The maximum length of the description is 200 characters.");
        }
    }
}