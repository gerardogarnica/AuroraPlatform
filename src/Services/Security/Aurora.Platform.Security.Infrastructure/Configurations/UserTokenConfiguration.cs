using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserToken", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_UserToken");

            builder.Property(e => e.Id).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.AccessToken).HasColumnName("AccessToken").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.AccessTokenExpiration).HasColumnName("AccessTokenExpiration").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.RefreshToken).HasColumnName("RefreshToken").IsRequired().HasColumnType("varchar(500)");
            builder.Property(e => e.RefreshTokenExpiration).HasColumnName("RefreshTokenExpiration").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.IssuedDate).HasColumnName("IssuedDate").IsRequired().HasColumnType("datetime");

            builder.HasOne(e => e.User).WithOne(e => e.Token).HasForeignKey<UserToken>(e => e.Id).HasConstraintName("FK_UserToken_User");
        }
    }
}