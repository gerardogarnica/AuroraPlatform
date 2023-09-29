using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_UserRole");

            builder.Property(e => e.Id).HasColumnName("UserRoleId").IsRequired().HasColumnType(SqlDataType.Int32).UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.RoleId).HasColumnName("RoleId").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.IsDefault).HasColumnName("IsDefault").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.AddAuditableProperties();

            builder.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique().HasDatabaseName("UK_UserRole");

            builder.HasOne(e => e.User).WithMany(e => e.UserRoles).HasForeignKey(e => e.UserId).HasConstraintName("FK_UserRole_User");
            builder.HasOne(e => e.Role).WithMany(e => e.UserRoles).HasForeignKey(e => e.RoleId).HasConstraintName("FK_UserRole_Role");
        }
    }
}