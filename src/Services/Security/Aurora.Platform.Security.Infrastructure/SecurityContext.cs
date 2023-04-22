using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Security.Infrastructure
{
    public class SecurityContext : DbContext
    {
        #region DbSet properties

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
        public DbSet<UserCredentialLog> UserCredentialLogs { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

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