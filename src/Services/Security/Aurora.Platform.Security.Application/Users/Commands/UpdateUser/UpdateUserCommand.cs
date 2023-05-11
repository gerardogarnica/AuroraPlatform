using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<int>
{
    public string LoginName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
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
        var user = await GetUserAsync(request.LoginName);

        // Update user entity
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        user = await _userRepository.UpdateAsync(user);

        // Returns entity ID
        return user.Id;
    }

    #endregion

    #region Private methods

    private async Task<User> GetUserAsync(string loginName)
    {
        var user = await _userRepository.GetAsync(loginName) ?? throw new InvalidUserNameException(loginName);
        user.CheckIfIsActive();

        return user;
    }

    #endregion
}