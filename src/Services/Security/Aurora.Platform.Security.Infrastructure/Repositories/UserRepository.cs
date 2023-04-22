using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Security.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region Private members

        private readonly SecurityContext _context;

        #endregion

        #region Constructors

        public UserRepository(SecurityContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IUserRepository implementation

        async Task<User> IUserRepository.GetAsync(string loginName)
        {
            return await _context
                .Users
                .AsNoTracking()
                .Include(x => x.Credential)
                .Include(x => x.Token)
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.LoginName.Equals(loginName));
        }

        #endregion
    }
}