using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Roles.Commands.UpdateRole;

public record UpdateRoleCommand : IRequest<int>
{
    public int RoleId { get; init; }
    public string Description { get; init; }
}

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, int>
{
    #region Private members

    private readonly IRoleRepository _roleRepository;

    #endregion

    #region Constructor

    public UpdateRoleHandler(
        IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<UpdateRoleCommand, int>.Handle(
        UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        // Get role
        var role = await GetRoleAsync(request.RoleId);

        // Update role entity
        role.Description = request.Description;

        role = await _roleRepository.UpdateAsync(role);

        // Returns entity ID
        return role.Id;
    }

    #endregion

    #region Private methods

    private async Task<Role> GetRoleAsync(int roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId) ?? throw new InvalidRoleIdentifierException(roleId);
        role.CheckIfIsActive();

        return role;
    }

    #endregion
}