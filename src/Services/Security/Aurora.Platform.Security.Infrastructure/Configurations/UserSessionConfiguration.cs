using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.ToTable("UserSession", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_UserSession");

            builder.Property(e => e.Id).HasColumnName("SessionId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.LoginName).HasColumnName("LoginName").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.AccessToken).HasColumnName("AccessToken").IsRequired().HasColumnType("varchar(4000)");
            builder.Property(e => e.AccessTokenExpiration).HasColumnName("AccessTokenExpiration").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.RefreshToken).HasColumnName("RefreshToken").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.RefreshTokenExpiration).HasColumnName("RefreshTokenExpiration").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.BeginSessionDate).HasColumnName("BeginSessionDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.EndSessionDate).HasColumnName("EndSessionDate").HasColumnType("datetime");
        }
    }
}