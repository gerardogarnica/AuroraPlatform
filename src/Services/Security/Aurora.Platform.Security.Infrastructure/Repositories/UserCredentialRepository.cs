using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;

namespace Aurora.Platform.Security.Infrastructure.Repositories
{
    public class UserCredentialRepository : RepositoryBase<UserCredential>, IUserCredentialRepository
    {
        #region Private members

        private readonly SecurityContext _context;

        #endregion

        #region Constructors

        public UserCredentialRepository(SecurityContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion
    }
}