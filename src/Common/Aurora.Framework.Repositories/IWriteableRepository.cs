﻿using Aurora.Framework.Entities;

namespace Aurora.Framework.Repositories
{
    public interface IWriteableRepository<T> : IAsyncRepository<T> where T : EntityBase
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}