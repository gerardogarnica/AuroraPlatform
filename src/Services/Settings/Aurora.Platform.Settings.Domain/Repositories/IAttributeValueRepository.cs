using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Domain.Repositories
{
    public interface IAttributeValueRepository : IReadableRepository<AttributeValue>, IWriteableRepository<AttributeValue>
    {
        Task<AttributeValue> GetByCodeAsync(string code, int relationshipId);
        Task<IReadOnlyList<AttributeValue>> GetListAsync(string scopeType, int relationshipId);
    }
}