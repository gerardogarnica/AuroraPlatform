using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Domain.Repositories
{
    public interface IOptionsListRepository : IReadableRepository<OptionsList>, IWriteableRepository<OptionsList>
    {
        Task<OptionsList?> GetByCodeAsync(string code);
    }
}