using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Queries.GetUserByGuid;

public record GetUserByGuidQuery : IRequest<UserInfo>
{
    public string Guid { get; init; }
}

public class GetUserByGuidQueryHandler : IRequestHandler<GetUserByGuidQuery, UserInfo>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public GetUserByGuidQueryHandler(
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

    async Task<UserInfo> IRequestHandler<GetUserByGuidQuery, UserInfo>.Handle(
        GetUserByGuidQuery request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetAsyncByGuid(request.Guid);
        if (user == null) return null;

        // Get user roles
        var userInfo = _mapper.Map<UserInfo>(user);
        userInfo.Roles = await GetUserRolesAsync(user.Id, user.UserRoles);

        // Returns user info
        return userInfo;
    }

    #endregion

    #region Private methods

    private async Task<List<RoleInfo>> GetUserRolesAsync(int userId, IList<UserRole> userRoles)
    {
        var roles = await _roleRepository.GetListAsync(userId);
        var rolesInfo = _mapper.Map<List<RoleInfo>>(roles);

        foreach (var roleInfo in rolesInfo)
        {
            if (!userRoles.Any(x => x.RoleId == roleInfo.RoleId)) continue;

            roleInfo.IsDefault = userRoles.First(x => x.RoleId == roleInfo.RoleId).IsDefault;
            roleInfo.IsActive = userRoles.First(x => x.RoleId == roleInfo.RoleId).IsActive;
        }

        return rolesInfo;
    }

    #endregion
}