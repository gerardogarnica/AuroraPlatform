using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Domain.Repositories
{
    public interface IAttributeSettingRepository : IReadableRepository<AttributeSetting>, IWriteableRepository<AttributeSetting>
    {
    }
}