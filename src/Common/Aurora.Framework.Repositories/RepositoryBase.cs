using Aurora.Framework.Entities;
using Aurora.Framework.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aurora.Framework.Repositories
{
    public abstract class RepositoryBase<T> : IReadableRepository<T>, IWriteableRepository<T>, IRemovableRepository<T> where T : EntityBase
    {
        #region Private members

        private readonly DbContext _context;

        #endregion

        #region Constructors

        public RepositoryBase(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IReadableRepository implementation

        async Task<T?> IReadableRepository<T>.GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }

        async Task<T?> IReadableRepository<T>.GetByIdAsync(int id)
        {
            return await _context
                .Set<T>()
                .FindAsync(id);
        }

        async Task<IReadOnlyList<T>> IReadableRepository<T>.GetAllAsync()
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        async Task<IReadOnlyList<T>> IReadableRepository<T>.GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        async Task<IReadOnlyList<T>> IReadableRepository<T>.GetListAsync<TS>(Expression<Func<T, bool>> predicate,
                                                                             Expression<Func<T, TS>> orderBy,
                                                                             bool descending)
        {
            IQueryable<T> query = _context
                .Set<T>()
                .AsNoTracking()
                .Where(predicate);

            return descending
                ? await query.OrderByDescending(orderBy).ToListAsync()
                : await query.OrderBy(orderBy).ToListAsync();
        }

        async Task<PagedCollection<T>> IReadableRepository<T>.GetPagedListAsync(PagedViewRequest viewRequest,
                                                                                Expression<Func<T, bool>> predicate)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .Where(predicate)
                .ToPagedCollectionAsync(viewRequest);
        }

        async Task<PagedCollection<T>> IReadableRepository<T>.GetPagedListAsync<TS>(PagedViewRequest viewRequest,
                                                                                    Expression<Func<T, bool>> predicate,
                                                                                    Expression<Func<T, TS>> orderBy,
                                                                                    bool descending)
        {
            IQueryable<T> query = _context
                .Set<T>()
                .AsNoTracking()
                .Where(predicate);

            return descending
                ? await query.OrderByDescending(orderBy).ToPagedCollectionAsync(viewRequest)
                : await query.OrderBy(orderBy).ToPagedCollectionAsync(viewRequest);
        }

        #endregion

        #region IWriteableRepository implementation

        async Task<T> IWriteableRepository<T>.AddAsync(T entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return entity;
        }

        async Task<T> IWriteableRepository<T>.UpdateAsync(T entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return entity;
        }

        #endregion

        #region IRemovableRepository implementation

        async Task IRemovableRepository<T>.DeleteAsync(T entity)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        #endregion

        private bool IsAuditableEntity(T entity)
        {
            return typeof(AuditableEntity).IsAssignableFrom(entity.GetType());
        }
    }
}