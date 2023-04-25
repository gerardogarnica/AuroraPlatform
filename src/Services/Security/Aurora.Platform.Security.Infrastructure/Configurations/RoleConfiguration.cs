using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_Role");

            builder.Property(e => e.Id).HasColumnName("RoleId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(e => e.IsGlobal).HasColumnName("IsGlobal").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.AddAuditableProperties();

            builder.HasIndex(e => e.Name).IsUnique().HasDatabaseName("UK_Role");
        }
    }
}