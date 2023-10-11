using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;

namespace Aurora.Platform.Security.Application.Roles.Commands.UpdateRoleStatus;

public record UpdateRoleStatusCommand : IRequest<int>
{
    public string Guid { get; init; }
    public bool IsActive { get; init; }
}

public class UpdateRoleStatusHandler : IRequestHandler<UpdateRoleStatusCommand, int>
{
    #region Private members

    private readonly IRoleRepository _roleRepository;

    #endregion

    #region Constructor

    public UpdateRoleStatusHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<UpdateRoleStatusCommand, int>.Handle(
        UpdateRoleStatusCommand request, CancellationToken cancellationToken)
    {
        // Get role
        var role = await _roleRepository.GetAsync(x => x.Guid.ToString().Equals(request.Guid))
            ?? throw new InvalidRoleGuidException(request.Guid);

        // Update role entity
        role.IsActive = request.IsActive;

        role = await _roleRepository.UpdateAsync(role);

        // Returns entity ID
        return role.Id;
    }

    #endregion
}