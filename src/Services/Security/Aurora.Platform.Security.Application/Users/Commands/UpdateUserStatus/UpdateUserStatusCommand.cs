using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Commands.UpdateUserStatus;

public record UpdateUserStatusCommand : IRequest<int>
{
    public string Email { get; init; }
    public bool IsActive { get; init; }
}

public class UpdateUserStatusHandler : IRequestHandler<UpdateUserStatusCommand, int>
{
    #region Private members

    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public UpdateUserStatusHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<UpdateUserStatusCommand, int>.Handle(
        UpdateUserStatusCommand request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await GetUserAsync(request.Email);

        // Update user entity
        user.IsActive = request.IsActive;

        user = await _userRepository.UpdateAsync(user);

        // Returns entity ID
        return user.Id;
    }

    #endregion

    #region Private methods

    private async Task<User> GetUserAsync(string email)
    {
        var user = await _userRepository.GetAsync(email) ?? throw new InvalidUserEmailException(email);
        user.CheckIfIsUnableToChange();

        return user;
    }

    #endregion
}