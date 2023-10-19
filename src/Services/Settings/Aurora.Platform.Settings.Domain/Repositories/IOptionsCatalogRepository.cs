using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Domain.Repositories
{
    public interface IOptionsCatalogRepository : IReadableRepository<OptionsCatalog>, IWriteableRepository<OptionsCatalog>
    {
        Task<OptionsCatalog> GetByCodeAsync(string code);
    }
}