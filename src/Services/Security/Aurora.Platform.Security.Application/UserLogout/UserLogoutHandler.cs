using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.UserLogout
{
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

            // Update user token
            await UpdateUserToken(user);

            // Update user session
            var entry = await UpdateUserSession(user);

            return entry.Id;
        }

        #endregion

        #region Private methods

        private async Task<UserSession> UpdateUserSession(UserInfo user)
        {
            var userSession = await _userSessionRepository.GetLastAsync(user.UserId);
            userSession.EndSessionDate = DateTime.UtcNow;

            return await _userSessionRepository.UpdateAsync(userSession);
        }

        private async Task UpdateUserToken(UserInfo user)
        {
            var tokenInfo = await _userTokenRepository.GetByIdAsync(user.UserId);
            tokenInfo.AccessToken = null;
            tokenInfo.RefreshToken = null;
            tokenInfo.AccessTokenExpiration = null;
            tokenInfo.RefreshTokenExpiration = null;
            tokenInfo.IssuedDate = DateTime.UtcNow;

            await _userTokenRepository.UpdateAsync(tokenInfo);
        }

        #endregion
    }
}