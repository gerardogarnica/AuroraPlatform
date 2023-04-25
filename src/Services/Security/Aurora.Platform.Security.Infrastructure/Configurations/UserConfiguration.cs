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
            builder.Property(e => e.LoginName).HasColumnName("LoginName").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.FirstName).HasColumnName("FirstName").IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(e => e.LastName).HasColumnName("LastName").IsRequired().HasColumnType("nvarchar(40)");
            builder.Property(e => e.Email).HasColumnName("Email").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.IsDefault).HasColumnName("IsDefault").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.AddAuditableProperties();

            builder.HasIndex(e => e.LoginName).IsUnique().HasDatabaseName("UK_User");
        }
    }
}