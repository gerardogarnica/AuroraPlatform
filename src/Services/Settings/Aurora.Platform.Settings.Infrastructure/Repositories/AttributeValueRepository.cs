using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;
using Aurora.Platform.Settings.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Settings.Infrastructure.Repositories
{
    public class AttributeValueRepository : RepositoryBase<AttributeValue>, IAttributeValueRepository
    {
        #region Private members

        private readonly SettingsContext _context;

        #endregion

        #region Constructors

        public AttributeValueRepository(SettingsContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IAttributeValueRepository implementation

        async Task<AttributeValue> IAttributeValueRepository.GetByCodeAsync(string code, int relationshipId)
        {
            return await _context
                .AttributeValues
                .Include(x => x.AttributeSetting)
                .FirstOrDefaultAsync(x => x.AttributeSetting.Code == code && x.RelationshipId == relationshipId);
        }

        async Task<IList<AttributeValue>> IAttributeValueRepository.GetListAsync(string scopeType, int relationshipId)
        {
            return await _context
                .AttributeValues
                .Include(x => x.AttributeSetting)
                .Where(x => x.AttributeSetting.ScopeType == scopeType && x.RelationshipId == relationshipId)
                .ToListAsync();
        }

        #endregion
    }
}