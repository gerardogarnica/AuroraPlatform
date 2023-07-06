using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Commands.UpdateUserRole;

public record UpdateUserRoleCommand : IRequest<int>
{
    public string Email { get; init; }
    public int RoleId { get; init; }
    public bool IsAddAction { get; init; }
}

public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleCommand, int>
{
    #region Private members

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    #endregion

    #region Constructor

    public UpdateUserRoleHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<UpdateUserRoleCommand, int>.Handle(
        UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await GetUserAsync(request.Email);

        // Get role
        var role = await GetRoleAsync(request.RoleId);

        // Update user entity
        if (user.UserRoles.Any(x => x.RoleId.Equals(role.Id)))
        {
            var userRole = user.UserRoles.First(x => x.RoleId.Equals(role.Id));

            if (userRole.IsDefault && !request.IsAddAction)
                user.CheckIfIsUnableToChange();

            userRole.IsActive = request.IsAddAction;
        }
        else
        {
            if (!request.IsAddAction) throw new UserDoesNotHaveRoleException(user.Email, role.Name);

            // Add new role to user
            user.UserRoles.Add(new UserRole(user.Id, role.Id, false, true));

            // Add new token to user
            if (!user.Tokens.Any(x => x.Application.Equals(role.Application)))
            {
                user.Tokens.Add(
                    new UserToken()
                    {
                        Application = role.Application,
                        IssuedDate = DateTime.UtcNow
                    });
            }
        }

        // Update user entity
        user = await _userRepository.UpdateAsync(user);

        // Returns entity ID
        return user.Id;
    }

    #endregion

    #region Private methods

    private async Task<User> GetUserAsync(string email)
    {
        var user = await _userRepository.GetAsync(email) ?? throw new InvalidUserEmailException(email);

        return user;
    }

    private async Task<Role> GetRoleAsync(int roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId) ?? throw new InvalidRoleIdentifierException(roleId);
        role.CheckIfIsActive();

        return role;
    }

    #endregion
}