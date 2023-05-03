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

        public UserSessionRepository(SecurityContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IUserSessionRepository implementation

        async Task<UserSession> IUserSessionRepository.GetLastAsync(int userId)
        {
            return await _context
                .UserSessions
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId));
        }

        #endregion
    }
}