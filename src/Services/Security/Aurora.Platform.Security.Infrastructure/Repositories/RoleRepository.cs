using Aurora.Framework.Entities;
using Aurora.Framework.Repositories;
using Aurora.Framework.Repositories.Extensions;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;

namespace Aurora.Platform.Security.Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        #region Private members

        private readonly SecurityContext _context;

        #endregion

        #region Constructors

        public RoleRepository(SecurityContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IRoleRepository implementation

        async Task<PagedCollection<Role>> IRoleRepository.GetListAsync(PagedViewRequest viewRequest, string application, bool onlyActives)
        {
            var query = from s in _context.Roles
                        where s.Application.Equals(application)
                        select s;

            if (onlyActives)
                query = query.Where(x => x.IsActive);

            return await query.ToPagedCollectionAsync(viewRequest);
        }

        #endregion
    }
}