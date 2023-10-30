using Aurora.Framework.Identity;
using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Settings.Infrastructure.Repositories
{
    public class OptionsCatalogRepository : RepositoryBase<OptionsCatalog>, IOptionsCatalogRepository
    {
        #region Private members

        private readonly SettingsContext _context;

        #endregion

        #region Constructors

        public OptionsCatalogRepository(SettingsContext context, IIdentityHandler identityHandler)
            : base(context, identityHandler)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IOptionsCatalogRepository implementation

        async Task<OptionsCatalog> IOptionsCatalogRepository.GetByCodeAsync(string code)
        {
            return await _context
                .Options
                .AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Code.Equals(code));
        }

        #endregion
    }
}