using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Roles.Queries.GetRoleByGuid;

public record GetRoleByGuidQuery : IRequest<RoleInfo>
{
    public string Guid { get; init; }
}

public class GetRoleByRoleHandler : IRequestHandler<GetRoleByGuidQuery, RoleInfo>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    #endregion

    #region Constructor

    public GetRoleByRoleHandler(
        IMapper mapper,
        IRoleRepository roleRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<RoleInfo> IRequestHandler<GetRoleByGuidQuery, RoleInfo>.Handle(
        GetRoleByGuidQuery request, CancellationToken cancellationToken)
    {
        // Get role
        var role = await _roleRepository.GetAsync(x => x.Guid.ToString().Equals(request.Guid));
        if (role == null) return null;

        // Returns role info
        return _mapper.Map<RoleInfo>(role);
    }

    #endregion
}