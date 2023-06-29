using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Security.Infrastructure.Repositories
{
    public class CredentialLogRepository : RepositoryBase<CredentialLog>, ICredentialLogRepository
    {
        #region Private members

        private readonly SecurityContext _context;

        #endregion

        #region Constructors

        public CredentialLogRepository(SecurityContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region ICredentialLogRepository implementation

        async Task<CredentialLog> ICredentialLogRepository.GetLastAsync(int userId)
        {
            return await _context
                .CredentialLogs
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId));
        }

        #endregion

    }
}