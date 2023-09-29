﻿using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Users.Queries.GetUserByEmail;

public record GetUserByEmailQuery : IRequest<UserInfo>
{
    public string Email { get; init; }
}

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserInfo>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public GetUserByEmailHandler(
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

    async Task<UserInfo> IRequestHandler<GetUserByEmailQuery, UserInfo>.Handle(
        GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetAsync(request.Email);
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
        var roles = new List<RoleInfo>();

        foreach (var userRole in userRoles)
        {
            var role = await _roleRepository.GetAsync(x => x.Id == userRole.RoleId);

            roles.Add(
                new RoleInfo()
                {
                    RoleId = role.Id,
                    AppCode = role.AppCode,
                    AppName = role.AppName,
                    Name = role.Name,
                    Description = role.Description,
                    Guid = role.Guid,
                    Notes = role.Notes,
                    IsDefault = userRole.IsDefault,
                    IsActive = userRole.IsActive
                });
        }

        return roles;
    }

    #endregion
}