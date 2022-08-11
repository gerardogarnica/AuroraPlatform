using Aurora.Framework.Entities;

namespace Aurora.Framework.Repositories
{
    public interface IRemovableRepository<T> : IAsyncRepository<T> where T : class, IEntity
    {
        Task DeleteAsync(T entity);
    }
}