using Aurora.Framework;
using Aurora.Framework.Entities;
using Aurora.Framework.Repositories;
using Aurora.Framework.Repositories.Extensions;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aurora.Platform.Security.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region Private members

        private readonly SecurityContext _context;

        #endregion

        #region Constructors

        public UserRepository(SecurityContext context)
            : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IUserRepository implementation

        async Task<User> IUserRepository.GetAsyncByEmail(string email)
        {
            return await GetAsync(x => x.Email.Equals(email));
        }

        async Task<User> IUserRepository.GetAsyncByGuid(string guid)
        {
            return await GetAsync(x => x.Guid.ToString().Equals(guid));
        }

        async Task<PagedCollection<User>> IUserRepository.GetListAsync(
            PagedViewRequest viewRequest, string search, bool onlyActives)
        {
            Expression<Func<User, bool>> predicate = x => x.Id == x.Id;
            if (!string.IsNullOrWhiteSpace(search) && search.Length >= 3)
                predicate = predicate.And(x => x.Email.Contains(search) || x.FirstName.Contains(search) || x.LastName.Contains(search));
            if (onlyActives)
                predicate = predicate.And(x => x.IsActive);

            return await _context
                .Users
                .Where(predicate)
                .OrderBy(x => x.FirstName)
                .ToPagedCollectionAsync(viewRequest);
        }

        async Task<PagedCollection<User>> IUserRepository.GetListAsync(
            PagedViewRequest viewRequest, int roleId, string search, bool onlyActives)
        {
            var ids = await _context
                .UserRoles
                .Where(x => x.IsActive && x.RoleId == roleId)
                .OrderBy(x => x.User.FirstName)
                .Skip(viewRequest.PageIndex * viewRequest.PageSize)
                .Take(viewRequest.PageSize)
                .Select(x => x.UserId)
                .ToArrayAsync();

            return await (from s in _context.Users
                          where ids.Contains(s.Id) &&
                          (string.IsNullOrEmpty(search) || EF.Functions.Like(s.FirstName, $"%{search}%") ||
                           EF.Functions.Like(s.LastName, $"%{search}%") || EF.Functions.Like(s.Email, $"%{search}%"))
                          select s)
                          .ToPagedCollectionAsync(viewRequest);
        }

        #endregion

        #region Private methods

        private async Task<User> GetAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context
                .Users
                .AsNoTracking()
                .Include(x => x.Tokens)
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(predicate);
        }

        #endregion
    }
}