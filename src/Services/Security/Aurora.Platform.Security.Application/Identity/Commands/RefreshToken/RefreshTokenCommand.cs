using Aurora.Framework.Security;
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

    private readonly IJwtSecurityHandler _securityHandler;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserSessionRepository _userSessionRepository;

    private readonly string applicationCode;

    #endregion

    #region Constructor

    public RefreshTokenHandler(
        IJwtSecurityHandler securityHandler,
        IUserTokenRepository userTokenRepository,
        IUserSessionRepository userSessionRepository)
    {
        _securityHandler = securityHandler;
        _userTokenRepository = userTokenRepository;
        _userSessionRepository = userSessionRepository;

        applicationCode = _securityHandler.GetApplicationCode();
    }

    #endregion

    #region IRequestHandler implementation

    async Task<IdentityToken> IRequestHandler<RefreshTokenCommand, IdentityToken>.Handle(
        RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Get logged user
        var user = _securityHandler.UserInfo;

        // Get token information
        var tokenInfo = _securityHandler.GenerateTokenInfo(user);

        // Get current user token
        var userToken = await _userTokenRepository
            .GetAsync(x => x.UserId == user.UserId && x.Application.Equals(applicationCode));
        userToken.CheckIfRefreshTokenIsValid(request.RefreshToken);
        userToken.CheckIfRefreshTokenIsNotExpired();

        // Update token repository
        userToken.UpdateWithTokenInfo(tokenInfo);
        await _userTokenRepository.UpdateAsync(userToken);

        // Update user session
        var userSession = await _userSessionRepository.GetLastAsync(user.UserId, applicationCode);
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