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

            builder.Property(e => e.Id).HasColumnName("RoleId").IsRequired().HasColumnType(SqlDataType.Int32).UseIdentityColumn();
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType(SqlDataType.VarChar50);
            builder.Property(e => e.AppCode).HasColumnName("AppCode").IsRequired().HasColumnType(SqlDataType.VarChar50);
            builder.Property(e => e.AppName).HasColumnName("AppName").IsRequired().HasColumnType(SqlDataType.VarChar50);
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType(SqlDataType.Description);
            builder.Property(e => e.Guid).HasColumnName("RoleGuid").IsRequired().HasDefaultValueSql("newId()").HasColumnType(SqlDataType.Guid);
            builder.Property(e => e.Notes).HasColumnName("Notes").HasColumnType(SqlDataType.Notes);
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.AddAuditableProperties();

            builder.HasIndex(e => new { e.Name, e.AppCode }).IsUnique().HasDatabaseName("UK_Role");
        }
    }
}