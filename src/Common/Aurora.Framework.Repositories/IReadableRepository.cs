using Aurora.Framework.Entities;
using System.Linq.Expressions;

namespace Aurora.Framework.Repositories
{
    public interface IReadableRepository<T> : IAsyncRepository<T> where T : class, IEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetListAsync<TS>(Expression<Func<T, bool>> predicate,
                                                Expression<Func<T, TS>> orderBy,
                                                bool descending = false);
        Task<PagedCollection<T>> GetPagedListAsync(PagedViewRequest viewRequest,
                                                   Expression<Func<T, bool>> predicate);
        Task<PagedCollection<T>> GetPagedListAsync<TS>(PagedViewRequest viewRequest,
                                                       Expression<Func<T, bool>> predicate,
                                                       Expression<Func<T, TS>> orderBy,
                                                       bool descending = false);
    }
}