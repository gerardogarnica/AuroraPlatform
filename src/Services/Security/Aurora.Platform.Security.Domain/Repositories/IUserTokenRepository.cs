using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Domain.Repositories
{
    public interface IUserTokenRepository : IReadableRepository<UserToken>, IWriteableRepository<UserToken>
    {
    }
}