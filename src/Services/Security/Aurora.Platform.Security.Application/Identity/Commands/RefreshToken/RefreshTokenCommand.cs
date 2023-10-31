using Aurora.Framework.Identity;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<IdentityToken>
{
    public string RefreshToken { get; set; }
}

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, IdentityToken>
{
    #region Private members

    private readonly IIdentityHandler _identityHandler;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserSessionRepository _userSessionRepository;

    #endregion

    #region Constructor

    public RefreshTokenHandler(
        IIdentityHandler identityHandler,
        IUserTokenRepository userTokenRepository,
        IUserSessionRepository userSessionRepository)
    {
        _identityHandler = identityHandler;
        _userTokenRepository = userTokenRepository;
        _userSessionRepository = userSessionRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<IdentityToken> IRequestHandler<RefreshTokenCommand, IdentityToken>.Handle(
        RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Get logged user
        var user = _identityHandler.UserInfo;

        // Set the application code
        var appCode = _identityHandler.ApplicationInfo.Code;

        // Get token information
        var tokenInfo = _identityHandler.GenerateTokenInfo(user, _identityHandler.ApplicationInfo);

        // Get current user token
        var userToken = await _userTokenRepository
            .GetAsync(x => x.UserId == user.UserId && x.Application.Equals(appCode));
        userToken.CheckIfRefreshTokenIsValid(request.RefreshToken);
        userToken.CheckIfRefreshTokenIsNotExpired();

        // Update token repository
        userToken.UpdateWithTokenInfo(tokenInfo);
        await _userTokenRepository.UpdateAsync(userToken);

        // Update user session
        var userSession = await _userSessionRepository.GetLastAsync(user.UserId, appCode);
        userSession.UpdateWithTokenInfo(tokenInfo);

        await _userSessionRepository.UpdateAsync(userSession);

        // Returns identity tokens
        return new IdentityToken()
        {
            AccessToken = tokenInfo.AccessToken,
            RefreshToken = tokenInfo.RefreshToken
        };
    }

    #endregion
}