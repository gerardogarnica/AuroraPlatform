using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class CredentialLogConfiguration : IEntityTypeConfiguration<CredentialLog>
    {
        public void Configure(EntityTypeBuilder<CredentialLog> builder)
        {
            builder.ToTable("CredentialLog", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_CredentialLog");

            builder.Property(e => e.Id).HasColumnName("LogId").IsRequired().HasColumnType(SqlDataType.Int32).UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasColumnType(SqlDataType.VarChar200);
            builder.Property(e => e.PasswordControl).HasColumnName("PasswordControl").IsRequired().HasColumnType(SqlDataType.VarChar1000);
            builder.Property(e => e.ChangeVersion).HasColumnName("ChangeVersion").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate").HasColumnType(SqlDataType.DateTime);
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType(SqlDataType.DateTime);
            builder.Property(e => e.EndDate).HasColumnName("EndDate").HasColumnType(SqlDataType.DateTime);
        }
    }
}