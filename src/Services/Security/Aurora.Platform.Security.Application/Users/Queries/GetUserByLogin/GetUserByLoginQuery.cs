using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Queries.GetUserByLogin;

public record GetUserByLoginQuery : IRequest<UserInfo>
{
    public string LoginName { get; init; }
}

public class GetUserByLoginHandler : IRequestHandler<GetUserByLoginQuery, UserInfo>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public GetUserByLoginHandler(
        IMapper mapper,
        IRoleRepository roleRepository,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<UserInfo> IRequestHandler<GetUserByLoginQuery, UserInfo>.Handle(
        GetUserByLoginQuery request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetAsync(request.LoginName);
        if (user == null) return null;

        // Get user roles
        var userInfo = _mapper.Map<UserInfo>(user);
        userInfo.Roles = await GetUserRolesAsync(user.UserRoles);

        // Returns user info
        return userInfo;
    }

    #endregion

    #region Private methods

    private async Task<List<RoleInfo>> GetUserRolesAsync(IList<UserRole> userRoles)
    {
        var roles = new List<Role>();

        foreach (var userRole in userRoles)
        {
            roles.Add(await _roleRepository.GetAsync(x => x.Id == userRole.RoleId));
        }

        return _mapper.Map<List<RoleInfo>>(roles);
    }

    #endregion
}