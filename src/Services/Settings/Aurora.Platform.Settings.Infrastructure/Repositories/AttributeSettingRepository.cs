using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Repositories;

namespace Aurora.Platform.Settings.Infrastructure.Repositories
{
    public class AttributeSettingRepository : RepositoryBase<AttributeSetting>, IAttributeSettingRepository
    {
        #region Private members

        private readonly SettingsContext _context;

        #endregion

        #region Constructors

        public AttributeSettingRepository(SettingsContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IAttributeSettingRepository implementation

        #endregion
    }
}