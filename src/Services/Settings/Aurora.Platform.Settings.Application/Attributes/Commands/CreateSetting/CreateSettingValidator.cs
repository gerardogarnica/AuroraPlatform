using Aurora.Framework;
using FluentValidation;

namespace Aurora.Platform.Settings.Application.Attributes.Commands.CreateSetting
{
    public class CreateSettingValidator : AbstractValidator<CreateSettingCommand>
    {
        public CreateSettingValidator()
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

            RuleFor(p => p.ScopeType)
                .NotEmpty().WithMessage("Scope type is required.")
                .MaximumLength(25).WithMessage("The maximum scope type length is 25 characters.");

            RuleFor(p => p.BooleanSetting)
                .NotNull().When(p => p.DataType == AuroraDataType.Boolean).WithMessage("The boolean setting value is undefined.");

            RuleFor(p => p.IntegerSetting)
                .NotNull().When(p => p.DataType == AuroraDataType.Integer).WithMessage("The integer setting value is undefined.");

            RuleFor(p => p.MoneySetting)
                .NotNull().When(p => p.DataType == AuroraDataType.Money).WithMessage("The money setting value is undefined.");

            RuleFor(p => p.NumericSetting)
                .NotNull().When(p => p.DataType == AuroraDataType.Numeric).WithMessage("The numeric setting value is undefined.");

            RuleFor(p => p.OptionsSetting)
                .NotNull().When(p => p.DataType == AuroraDataType.Options).WithMessage("The options setting value is undefined.");

            RuleFor(p => p.TextSetting)
                .NotNull().When(p => p.DataType == AuroraDataType.Text).WithMessage("The text setting value is undefined.");
        }
    }
}