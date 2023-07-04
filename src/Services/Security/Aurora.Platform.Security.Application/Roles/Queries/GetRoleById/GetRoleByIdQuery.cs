using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery : IRequest<RoleInfo>
{
    public int RoleId { get; init; }
}

public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleInfo>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    #endregion

    #region Constructor

    public GetRoleByIdHandler(
        IMapper mapper,
        IRoleRepository roleRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<RoleInfo> IRequestHandler<GetRoleByIdQuery, RoleInfo>.Handle(
        GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        // Get role
        var role = await _roleRepository.GetByIdAsync(request.RoleId);
        if (role == null) return null;

        // Returns role info
        return _mapper.Map<RoleInfo>(role);
    }

    #endregion
}