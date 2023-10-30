using Aurora.Framework.Identity;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.ChangePassword;

public record ChangePasswordCommand : IRequest<bool>
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    #region Private members

    private readonly IIdentityHandler _identityHandler;
    private readonly IUserRepository _userRepository;
    private readonly ICredentialLogRepository _credentialLogRepository;

    #endregion

    #region Constructor

    public ChangePasswordHandler(
        IIdentityHandler identityHandler,
        IUserRepository userRepository,
        ICredentialLogRepository credentialLogRepository)
    {
        _identityHandler = identityHandler;
        _userRepository = userRepository;
        _credentialLogRepository = credentialLogRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<bool> IRequestHandler<ChangePasswordCommand, bool>.Handle(
        ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        // Get logged user
        var user = await GetUserAsync(request.CurrentPassword);

        // TODO: get expiration days from configuration
        // Update password credential
        user.EncryptPassword(request.NewPassword, DateTime.Today);

        // Update user repository
        user = await _userRepository.UpdateAsync(user);

        // Update current credential log
        var currentCredential = await _credentialLogRepository.GetLastAsync(user.Id);
        currentCredential.EndDate = DateTime.UtcNow;

        await _credentialLogRepository.UpdateAsync(currentCredential);

        // Add credential log repository
        var newCredential = new CredentialLog(user, currentCredential.ChangeVersion);
        await _credentialLogRepository.AddAsync(newCredential);

        // Returns result
        return true;
    }

    #endregion

    #region Private methods

    private async Task<User> GetUserAsync(string password)
    {
        var email = _identityHandler.UserInfo.Email;
        var user = await _userRepository.GetAsyncByEmail(email) ?? throw new InvalidUserEmailException(email);

        user.CheckIfPasswordMatches(password);
        user.CheckIfIsActive();

        return user;
    }

    #endregion
}