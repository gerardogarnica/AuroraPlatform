using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<int>
{
    public string Email { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Notes { get; init; }
}

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, int>
{
    #region Private members

    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<UpdateUserCommand, int>.Handle(
        UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await GetUserAsync(request.Email);

        // Update user entity
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Notes = request.Notes;

        user = await _userRepository.UpdateAsync(user);

        // Returns entity ID
        return user.Id;
    }

    #endregion

    #region Private methods

    private async Task<User> GetUserAsync(string email)
    {
        var user = await _userRepository.GetAsyncByEmail(email) ?? throw new InvalidUserEmailException(email);
        user.CheckIfIsUnableToChange();
        user.CheckIfIsActive();

        return user;
    }

    #endregion
}