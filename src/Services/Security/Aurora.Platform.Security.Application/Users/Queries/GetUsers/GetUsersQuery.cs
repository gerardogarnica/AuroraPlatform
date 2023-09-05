using Aurora.Framework.Entities;
using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<PagedCollection<UserInfo>>
{
    public PagedViewRequest PagedViewRequest { get; init; }
    public int RoleId { get; init; }
    public string Search { get; init; }
    public bool OnlyActives { get; init; }
}

public class GetUsersHandler : IRequestHandler<GetUsersQuery, PagedCollection<UserInfo>>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public GetUsersHandler(
        IMapper mapper,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<PagedCollection<UserInfo>> IRequestHandler<GetUsersQuery, PagedCollection<UserInfo>>.Handle(
        GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository
            .GetListAsync(request.PagedViewRequest, request.RoleId, request.Search, request.OnlyActives);

        return _mapper.Map<PagedCollection<UserInfo>>(users);
    }

    #endregion
}