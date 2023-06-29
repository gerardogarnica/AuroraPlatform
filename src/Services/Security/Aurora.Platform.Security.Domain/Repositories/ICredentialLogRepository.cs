using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Domain.Repositories
{
    public interface ICredentialLogRepository : IReadableRepository<CredentialLog>, IWriteableRepository<CredentialLog>
    {
        Task<CredentialLog> GetLastAsync(int userId);
    }
}