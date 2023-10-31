using Aurora.Framework.Identity;
using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;

namespace Aurora.Platform.Security.Infrastructure.Repositories
{
    public class ApplicationRepository : RepositoryBase<Application>, IApplicationRepository
    {
        #region Private members

        private readonly SecurityContext _context;

        #endregion

        #region Constructors

        public ApplicationRepository(SecurityContext context, IIdentityHandler identityHandler)
            : base(context, identityHandler)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion
    }
}