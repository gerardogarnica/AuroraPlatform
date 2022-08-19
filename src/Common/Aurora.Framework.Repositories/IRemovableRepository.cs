using Aurora.Framework.Entities;

namespace Aurora.Framework.Repositories
{
    public interface IRemovableRepository<T> : IAsyncRepository<T> where T : EntityBase
    {
        Task DeleteAsync(T entity);
    }
}