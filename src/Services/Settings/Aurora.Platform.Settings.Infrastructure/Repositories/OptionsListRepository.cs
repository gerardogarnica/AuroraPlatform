using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Settings.Infrastructure.Repositories
{
    public class OptionsListRepository : RepositoryBase<OptionsList>, IOptionsListRepository
    {
        #region Private members

        private readonly SettingsContext _context;

        #endregion

        #region Constructors

        public OptionsListRepository(SettingsContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IOptionsListRepository implementation

        async Task<OptionsList> IOptionsListRepository.GetByCodeAsync(string code)
        {
            return await _context
                .Options
                .AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Code == code);
        }

        #endregion
    }
}