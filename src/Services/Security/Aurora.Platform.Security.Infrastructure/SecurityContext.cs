using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Security.Infrastructure
{
    public class SecurityContext : DbContext
    {
        #region DbSet properties

        #endregion

        #region Constructors

        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options) { }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}