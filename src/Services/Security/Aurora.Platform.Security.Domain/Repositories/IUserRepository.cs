using Aurora.Framework.Entities;
using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Domain.Repositories
{
    public interface IUserRepository : IReadableRepository<User>, IWriteableRepository<User>
    {
        Task<User> GetAsyncByEmail(string email);
        Task<User> GetAsyncByGuid(string guid);
        Task<PagedCollection<User>> GetListAsync(PagedViewRequest viewRequest, string search, bool onlyActives);
        Task<PagedCollection<User>> GetListAsync(PagedViewRequest viewRequest, int roleId, string search, bool onlyActives);
    }
}