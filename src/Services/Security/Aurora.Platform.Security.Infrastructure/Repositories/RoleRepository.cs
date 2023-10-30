using Aurora.Framework.Identity;
using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Security.Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        #region Private members

        private readonly SecurityContext _context;

        #endregion

        #region Constructors

        public RoleRepository(SecurityContext context, IIdentityHandler identityHandler)
            : base(context, identityHandler)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IRoleRepository implementation

        async Task<IReadOnlyList<Role>> IRoleRepository.GetListAsync(int userId)
        {
            var ids = await _context
                .UserRoles
                .Where(x => x.IsActive && x.UserId == userId)
                .OrderBy(x => x.Role.Name)
                .Select(x => x.RoleId)
                .ToArrayAsync();

            return await (from s in _context.Roles
                          where ids.Contains(s.Id)
                          select s)
                          .ToListAsync();
        }

        #endregion
    }
}