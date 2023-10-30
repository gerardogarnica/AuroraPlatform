using Aurora.Framework.Identity;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Queries.GetProfile;

public record GetProfileQuery : IRequest<UserInfo> { }

public class GetProfileHandler : IRequestHandler<GetProfileQuery, UserInfo>
{
    #region Private members

    private readonly IIdentityHandler _identityHandler;
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public GetProfileHandler(
        IIdentityHandler identityHandler,
        IMapper mapper,
        IRoleRepository roleRepository,
        IUserRepository userRepository)
    {
        _identityHandler = identityHandler;
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
        var user = await _userRepository.GetAsyncByGuid(_identityHandler.UserInfo.Guid.ToString());
        if (user == null) return null;

        // Get user roles
        var userInfo = _mapper.Map<UserInfo>(user);
        userInfo.Roles = _identityHandler.UserInfo.Roles;

        // Returns user info
        return userInfo;
    }

    #endregion
}