using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Roles.Queries.GetRoles;

public record GetRolesQuery : IRequest<PagedCollection<RoleInfo>>
{
    public PagedViewRequest PagedViewRequest { get; init; }
    public string Application { get; init; }
    public bool OnlyActives { get; init; }
}

public class GetRolesHandler : IRequestHandler<GetRolesQuery, PagedCollection<RoleInfo>>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    #endregion

    #region Constructor

    public GetRolesHandler(
        IMapper mapper,
        IRoleRepository roleRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<PagedCollection<RoleInfo>> IRequestHandler<GetRolesQuery, PagedCollection<RoleInfo>>.Handle(
               GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository
            .GetListAsync(request.PagedViewRequest, request.Application, request.OnlyActives);

        return _mapper.Map<PagedCollection<RoleInfo>>(roles);
    }

    #endregion
}