using Aurora.Framework;
using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace Aurora.Platform.Security.Application.Roles.Queries.GetRoles;

public record GetRolesQuery : IRequest<PagedCollection<RoleInfo>>
{
    public PagedViewRequest PagedViewRequest { get; init; }
    public string Application { get; init; }
    public string Search { get; init; }
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
        Expression<Func<Role, bool>> predicate = x => x.Id == x.Id;

        if (!string.IsNullOrWhiteSpace(request.Application))
            predicate = predicate.And(x => x.AppCode.Equals(request.Application));
        if (!string.IsNullOrWhiteSpace(request.Search) && request.Search.Length >= 3)
            predicate = predicate.And(x => x.Name.Contains(request.Search) || x.Description.Contains(request.Search));
        if (request.OnlyActives)
            predicate = predicate.And(x => x.IsActive);

        var roles = await _roleRepository
            .GetPagedListAsync(request.PagedViewRequest, predicate, x => x.Name);

        return _mapper.Map<PagedCollection<RoleInfo>>(roles);
    }

    #endregion
}