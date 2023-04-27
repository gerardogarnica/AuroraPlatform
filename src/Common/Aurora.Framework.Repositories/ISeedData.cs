using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Repositories
{
    public interface ISeedData<T> where T : DbContext
    {
        void Seed(T context);
    }
}