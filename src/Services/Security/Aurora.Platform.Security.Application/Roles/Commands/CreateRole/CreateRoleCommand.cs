using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : IRequest<int>
{
    public string Name { get; init; }
    public string Application { get; init; }
    public string Description { get; init; }
    public string Notes { get; init; }
}

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, int>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    #endregion

    #region Constructor

    public CreateRoleHandler(
        IMapper mapper,
        IRoleRepository roleRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<int> IRequestHandler<CreateRoleCommand, int>.Handle(
        CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await CheckIfNameIsAvailable(request.Name, request.Application);

        // Create role entity
        var role = _mapper.Map<Role>(request);
        role.IsActive = true;

        // Add role repository
        role = await _roleRepository.AddAsync(role);

        // Returns entity ID
        return role.Id;
    }

    #endregion

    #region Private methods

    private async Task CheckIfNameIsAvailable(string name, string application)
    {
        var role = await _roleRepository.GetAsync(x => x.Name.Equals(name) && x.Application.Equals(application));
        if (role != null)
            throw new RoleNameAlreadyExistsException(name, application);
    }

    #endregion
}