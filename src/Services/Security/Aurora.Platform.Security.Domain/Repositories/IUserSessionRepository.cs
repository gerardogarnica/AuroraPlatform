using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Domain.Repositories
{
    public interface IUserSessionRepository : IReadableRepository<UserSession>, IWriteableRepository<UserSession>
    {
        Task<UserSession> GetLastAsync(int userId);
    }
}