using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Domain.Repositories
{
    public interface IUserRepository : IReadableRepository<User>, IWriteableRepository<User>
    {
        Task<User> GetAsync(string loginName);
    }
}