using Aurora.Framework.Cryptography;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        #region Private members

        private readonly IJwtSecurityHandler _securityHandler;
        private readonly IUserRepository _userRepository;
        private readonly IUserCredentialRepository _userCredentialRepository;

        #endregion

        #region Constructor

        public ChangePasswordHandler(
            IJwtSecurityHandler securityHandler,
            IUserRepository userRepository,
            IUserCredentialRepository userCredentialRepository)
        {
            _securityHandler = securityHandler;
            _userRepository = userRepository;
            _userCredentialRepository = userCredentialRepository;
        }

        #endregion

        #region IRequestHandler implementation

        async Task<bool> IRequestHandler<ChangePasswordCommand, bool>.Handle(
            ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // Get logged user
            var entry = await GetUserAsync(request.CurrentPassword);

            // Update credential repository
            await UpdateUserCredentialAsync(entry.Credential, request.NewPassword);

            return true;
        }

        #endregion

        #region Private methods

        private async Task<User> GetUserAsync(string password)
        {
            var loginName = _securityHandler.UserInfo.LoginName;
            var user = await _userRepository.GetAsync(loginName) ?? throw new InvalidUserNameException(loginName);

            user.CheckIfPasswordMatches(password);
            user.CheckIfIsActive();

            return user;
        }

        private async Task<UserCredential> UpdateUserCredentialAsync(UserCredential credential, string newPassword)
        {
            var passwordTuple = SymmetricEncryptionProvider.Protect(newPassword, "Aurora.Platform.Security.UserData");
            credential.Password = passwordTuple.Item1;
            credential.PasswordControl = passwordTuple.Item2;
            credential.CredentialLogs.Add(AddCredentialLog(passwordTuple.Item1, passwordTuple.Item2));

            return await _userCredentialRepository.UpdateAsync(credential);
        }

        private static UserCredentialLog AddCredentialLog(string password, string control)
        {
            return new UserCredentialLog()
            {
                ChangeVersion = 1,
                Password = password,
                PasswordControl = control,
                MustChange = false,
                CreatedDate = DateTime.UtcNow
            };
        }

        #endregion
    }
}