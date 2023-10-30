using Aurora.Framework.Identity;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.UserLogout;

public record UserLogoutCommand : IRequest<int> { }

public class UserLogoutHandler : IRequestHandler<UserLogoutCommand, int>
{
    #region Private members

    private readonly IIdentityHandler _identityHandler;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserSessionRepository _userSessionRepository;

    private readonly ApplicationInfo _application;

    #endregion

    #region Constructor

    public UserLogoutHandler(
        IIdentityHandler identityHandler,
        IUserTokenRepository userTokenRepository,
        IUserSessionRepository userSessionRepository)
    {
        _identityHandler = identityHandler;
        _userTokenRepository = userTokenRepository;
        _userSessionRepository = userSessionRepository;

        _application = _identityHandler.ApplicationInfo;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<UserLogoutCommand, int>.Handle(
        UserLogoutCommand request, CancellationToken cancellationToken)
    {
        // Get logged user
        var user = _identityHandler.UserInfo;

        // Get current user token
        var userToken = await _userTokenRepository
            .GetAsync(x => x.UserId == user.UserId && x.Application.Equals(_application.Code));

        // Update token repository
        userToken.ClearTokenInfo();
        await _userTokenRepository.UpdateAsync(userToken);

        // Update user session
        var userSession = await _userSessionRepository.GetLastAsync(user.UserId, _application.Code);
        userSession.EndSessionDate = DateTime.UtcNow;

        await _userSessionRepository.UpdateAsync(userSession);

        // Returns session ID
        return userSession.Id;
    }

    #endregion
}