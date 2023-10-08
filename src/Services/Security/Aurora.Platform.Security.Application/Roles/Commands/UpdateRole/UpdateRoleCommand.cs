using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Roles.Commands.UpdateRole;

public record UpdateRoleCommand : IRequest<int>
{
    public int RoleId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Notes { get; init; }
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
        var role = await GetRoleAsync(request.RoleId, request.Name);

        // Update role entity
        role.Name = request.Name;
        role.Description = request.Description;
        role.Notes = request.Notes;

        role = await _roleRepository.UpdateAsync(role);

        // Returns entity ID
        return role.Id;
    }

    #endregion

    #region Private methods

    private async Task<Role> GetRoleAsync(int roleId, string name)
    {
        var role = await _roleRepository.GetByIdAsync(roleId) ?? throw new InvalidRoleIdentifierException(roleId);

        var anotherExistingRole = await _roleRepository.GetAsync(x => x.Id != role.Id && x.Name.Equals(name) && x.AppCode.Equals(role.AppCode));
        if (anotherExistingRole != null) throw new RoleNameAlreadyExistsException(name, role.AppName);

        role.CheckIfIsActive();

        return role;
    }

    #endregion
}