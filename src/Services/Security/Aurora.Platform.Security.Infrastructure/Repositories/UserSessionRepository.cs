﻿using Aurora.Framework.Identity;
using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Security.Infrastructure.Repositories
{
    public class UserSessionRepository : RepositoryBase<UserSession>, IUserSessionRepository
    {
        #region Private members

        private readonly SecurityContext _context;

        #endregion

        #region Constructors

        public UserSessionRepository(SecurityContext context, IIdentityHandler identityHandler)
            : base(context, identityHandler)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IUserSessionRepository implementation

        async Task<UserSession> IUserSessionRepository.GetLastAsync(int userId, string application)
        {
            return await _context
                .UserSessions
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.Application.Equals(application));
        }

        #endregion
    }
}