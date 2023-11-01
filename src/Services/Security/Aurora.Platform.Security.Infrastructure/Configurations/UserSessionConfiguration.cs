using Aurora.Framework.Repositories;
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

            builder.Property(e => e.Id).HasColumnName("SessionId").IsRequired().HasColumnType(SqlDataType.Int32).UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.Application).HasColumnName("Application").IsRequired().HasColumnType(SqlDataType.Code);
            builder.Property(e => e.Email).HasColumnName("Email").IsRequired().HasColumnType(SqlDataType.Email);
            builder.Property(e => e.AccessToken).HasColumnName("AccessToken").IsRequired().HasColumnType(SqlDataType.VarChar4000);
            builder.Property(e => e.AccessTokenExpiration).HasColumnName("AccessTokenExpiration").IsRequired().HasColumnType(SqlDataType.DateTime);
            builder.Property(e => e.RefreshToken).HasColumnName("RefreshToken").IsRequired().HasColumnType(SqlDataType.VarChar200);
            builder.Property(e => e.RefreshTokenExpiration).HasColumnName("RefreshTokenExpiration").IsRequired().HasColumnType(SqlDataType.DateTime);
            builder.Property(e => e.BeginSessionDate).HasColumnName("BeginSessionDate").IsRequired().HasColumnType(SqlDataType.DateTime);
            builder.Property(e => e.EndSessionDate).HasColumnName("EndSessionDate").HasColumnType(SqlDataType.DateTime);
        }
    }
}