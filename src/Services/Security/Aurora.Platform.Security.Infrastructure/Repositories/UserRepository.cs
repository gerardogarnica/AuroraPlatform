using Aurora.Framework.Entities;
using Aurora.Framework.Repositories;
using Aurora.Framework.Repositories.Extensions;
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

        async Task<User> IUserRepository.GetAsync(string email)
        {
            return await _context
                .Users
                .AsNoTracking()
                .Include(x => x.Tokens)
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        async Task<PagedCollection<User>> IUserRepository.GetListAsync(PagedViewRequest viewRequest, int roleId, bool onlyActives)
        {
            var ids = await _context
                .UserRoles
                .Where(x => x.IsActive && roleId > 0
                    ? x.RoleId.Equals(roleId)
                    : x.RoleId.Equals(x.RoleId))
                .OrderBy(x => x.User.FirstName)
                .Skip(viewRequest.PageIndex * viewRequest.PageSize)
                .Take(viewRequest.PageSize)
                .Select(x => x.UserId)
                .ToArrayAsync();

            return await (from s in _context.Users
                          where ids.Contains(s.Id)
                          select s)
                          .ToPagedCollectionAsync(viewRequest);
        }

        #endregion
    }
}