using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Commands.UpdateUserRole;

public record UpdateUserRoleCommand : IRequest<int>
{
    public string UserGuid { get; init; }
    public string RoleGuid { get; init; }
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
        var user = await GetUserAsync(request.UserGuid);

        // Get role
        var role = await GetRoleAsync(request.RoleGuid);

        // Update user entity
        var userRole = user.GetUserRole(role.Id);
        if (userRole == null)
            user.AddUserRole(role, request.IsAddAction);
        else
            userRole.UpdateStatus(request.IsAddAction);

        user = await _userRepository.UpdateAsync(user);

        // Returns entity ID
        return user.Id;
    }

    #endregion

    #region Private methods

    private async Task<User> GetUserAsync(string guid)
    {
        var user = await _userRepository.GetAsyncByGuid(guid) ?? throw new InvalidUserGuidException(guid);

        return user;
    }

    private async Task<Role> GetRoleAsync(string roleGuid)
    {
        var role = await _roleRepository.GetAsync(x => x.Guid.ToString().Equals(roleGuid))
            ?? throw new InvalidRoleGuidException(roleGuid);
        role.CheckIfIsActive();

        return role;
    }

    #endregion
}