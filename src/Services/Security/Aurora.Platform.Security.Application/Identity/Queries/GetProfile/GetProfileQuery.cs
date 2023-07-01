using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Queries.GetProfile;

public record GetProfileQuery : IRequest<UserInfo> { }

public class GetProfileHandler : IRequestHandler<GetProfileQuery, UserInfo>
{
    #region Private members

    private readonly IJwtSecurityHandler _securityHandler;
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public GetProfileHandler(
        IJwtSecurityHandler securityHandler,
        IMapper mapper,
        IRoleRepository roleRepository,
        IUserRepository userRepository)
    {
        _securityHandler = securityHandler;
        _mapper = mapper;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<UserInfo> IRequestHandler<GetProfileQuery, UserInfo>.Handle(
        GetProfileQuery request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetAsync(_securityHandler.UserInfo.Email);
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