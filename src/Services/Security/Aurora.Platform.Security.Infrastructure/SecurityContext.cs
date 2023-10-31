using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Security.Infrastructure
{
    public class SecurityContext : DbContext
    {
        #region DbSet properties

        public DbSet<Application> Applications { get; set; }
        public DbSet<CredentialLog> CredentialLogs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
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

            modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
            modelBuilder.ApplyConfiguration(new CredentialLogConfiguration());

            /*
            modelBuilder.HasSequence<int>("UserSequence", "SEC");
            modelBuilder.Entity<User>().Property(o => o.Id).HasDefaultValueSql("NEXT VALUE FOR UserSequence");
            */
        }
    }
}