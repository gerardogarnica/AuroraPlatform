using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_User");

            builder.Property(e => e.Id).HasColumnName("UserId").IsRequired().HasColumnType(SqlDataType.Int32).UseIdentityColumn();
            builder.Property(e => e.FirstName).HasColumnName("FirstName").IsRequired().HasColumnType(SqlDataType.ProperName);
            builder.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasColumnType(SqlDataType.ProperName);
            builder.Property(e => e.Email).HasColumnName("Email").IsRequired().HasColumnType(SqlDataType.Email);
            builder.Property(e => e.Guid).HasColumnName("UserGuid").IsRequired().HasDefaultValueSql("newId()").HasColumnType(SqlDataType.Guid);
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasColumnType(SqlDataType.VarChar200);
            builder.Property(e => e.PasswordControl).HasColumnName("PasswordControl").IsRequired().HasColumnType(SqlDataType.VarChar1000);
            builder.Property(e => e.PasswordExpirationDate).HasColumnName("ExpirationDate").HasColumnType(SqlDataType.DateTime);
            builder.Property(e => e.Notes).HasColumnName("Notes").HasColumnType(SqlDataType.Notes);
            builder.Property(e => e.IsDefault).HasColumnName("IsDefault").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.AddAuditableProperties();

            builder.HasIndex(e => e.Email).IsUnique().HasDatabaseName("UK_User");
        }
    }
}