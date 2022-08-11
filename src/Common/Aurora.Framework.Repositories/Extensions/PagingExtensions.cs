using Aurora.Framework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Repositories.Extensions
{
    public static class PagingExtensions
    {
        public static async Task<PagedCollection<T>> ToPagedCollectionAsync<T>(this IQueryable<T> query,
                                                                               PagedViewRequest viewRequest) where T : class
        {
            var startingElement = 0;
            if (viewRequest.PageIndex > 0) startingElement = viewRequest.PageIndex * viewRequest.PageSize;

            var result = new PagedCollection<T>
            {
                Items = await query.Skip(startingElement).Take(viewRequest.PageSize).ToListAsync(),
                TotalItems = await query.CountAsync(),
                CurrentPage = viewRequest.PageIndex + 1
            };

            if (result.TotalItems > 0)
            {
                result.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.TotalItems) / viewRequest.PageSize));
            }

            return result;
        }
    }
}
