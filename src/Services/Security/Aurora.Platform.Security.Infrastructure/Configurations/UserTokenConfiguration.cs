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

            builder.Property(e => e.Id).HasColumnName("TokenId").IsRequired().HasColumnType("int");
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.Application).HasColumnName("Application").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.AccessToken).HasColumnName("AccessToken").HasColumnType("varchar(4000)");
            builder.Property(e => e.AccessTokenExpiration).HasColumnName("AccessTokenExpiration").HasColumnType("datetime");
            builder.Property(e => e.RefreshToken).HasColumnName("RefreshToken").HasColumnType("varchar(200)");
            builder.Property(e => e.RefreshTokenExpiration).HasColumnName("RefreshTokenExpiration").HasColumnType("datetime");
            builder.Property(e => e.IssuedDate).HasColumnName("IssuedDate").HasColumnType("datetime");

            builder.HasIndex(e => new { e.UserId, e.Application }).IsUnique().HasDatabaseName("UK_UserToken");

            builder.HasOne(e => e.User).WithMany(e => e.Tokens).HasForeignKey(e => e.UserId).HasConstraintName("FK_UserToken_User");
        }
    }
}