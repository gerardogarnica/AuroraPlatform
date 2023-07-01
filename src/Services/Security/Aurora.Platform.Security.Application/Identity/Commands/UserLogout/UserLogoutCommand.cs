using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.UserLogout;

public record UserLogoutCommand : IRequest<int> { }

public class UserLogoutHandler : IRequestHandler<UserLogoutCommand, int>
{
    #region Private members

    private readonly IJwtSecurityHandler _securityHandler;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserSessionRepository _userSessionRepository;

    #endregion

    #region Constructor

    public UserLogoutHandler(
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

    async Task<int> IRequestHandler<UserLogoutCommand, int>.Handle(
        UserLogoutCommand request, CancellationToken cancellationToken)
    {
        // Get logged user
        var user = _securityHandler.UserInfo;

        // Get current user token
        var userToken = await _userTokenRepository.GetByIdAsync(user.UserId);

        // Update token repository
        userToken.ClearTokenInfo();
        await _userTokenRepository.UpdateAsync(userToken);

        // Update user session
        var userSession = await _userSessionRepository.GetLastAsync(user.UserId);
        userSession.EndSessionDate = DateTime.UtcNow;

        await _userSessionRepository.UpdateAsync(userSession);

        // Returns session ID
        return userSession.Id;
    }

    #endregion
}