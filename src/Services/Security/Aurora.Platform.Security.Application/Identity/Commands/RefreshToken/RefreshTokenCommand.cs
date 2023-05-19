using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
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
    }

    #endregion

    #region IRequestHandler implementation

    async Task<IdentityToken> IRequestHandler<RefreshTokenCommand, IdentityToken>.Handle(
        RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Get current user token
        var entry = await _userTokenRepository.GetByIdAsync(_securityHandler.UserInfo.UserId);
        entry.CheckIfRefreshTokenIsValid(request.RefreshToken);
        entry.CheckIfRefreshTokenIsNotExpired();

        // Get token information
        var tokenInfo = _securityHandler.GenerateTokenInfo(_securityHandler.UserInfo);

        // Update token repository
        await UpdateUserTokenAsync(entry, tokenInfo);

        // Update user session
        await UpdateUserSessionAsync(_securityHandler.UserInfo.UserId, entry);

        // Returns identity tokens
        return new IdentityToken()
        {
            AccessToken = tokenInfo.AccessToken,
            RefreshToken = tokenInfo.RefreshToken
        };
    }

    #endregion

    #region Private methods

    private async Task UpdateUserTokenAsync(UserToken userToken, TokenInfo tokenInfo)
    {
        userToken.AccessToken = tokenInfo.AccessToken;
        userToken.AccessTokenExpiration = tokenInfo.AccessTokenExpiration;
        userToken.RefreshToken = tokenInfo.RefreshToken;
        userToken.RefreshTokenExpiration = tokenInfo.RefreshTokenExpiration;
        userToken.IssuedDate = DateTime.UtcNow;

        await _userTokenRepository.UpdateAsync(userToken);
    }

    private async Task UpdateUserSessionAsync(int userId, UserToken userToken)
    {
        var userSession = await _userSessionRepository.GetLastAsync(userId);

        userSession.AccessToken = userToken.AccessToken;
        userSession.AccessTokenExpiration = userToken.AccessTokenExpiration.Value;
        userSession.RefreshToken = userToken.RefreshToken;
        userSession.RefreshTokenExpiration = userToken.RefreshTokenExpiration.Value;

        await _userSessionRepository.UpdateAsync(userSession);
    }

    #endregion
}