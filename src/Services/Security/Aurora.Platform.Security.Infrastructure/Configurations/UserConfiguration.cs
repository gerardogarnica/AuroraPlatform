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

            builder.Property(e => e.Id).HasColumnName("UserId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.FirstName).HasColumnName("FirstName").IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(e => e.Email).HasColumnName("Email").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.PasswordControl).HasColumnName("PasswordControl").IsRequired().HasColumnType("varchar(1000)");
            builder.Property(e => e.PasswordExpirationDate).HasColumnName("ExpirationDate").HasColumnType("datetime");
            builder.Property(e => e.IsDefault).HasColumnName("IsDefault").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.AddAuditableProperties();

            builder.HasIndex(e => e.Email).IsUnique().HasDatabaseName("UK_User");
        }
    }
}