using FluentValidation;

namespace Aurora.Platform.Security.Application.Identity.Commands.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(p => p.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }
}