using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Settings.Infrastructure.Configuration
{
    public class AttributeSettingConfiguration : IEntityTypeConfiguration<AttributeSetting>
    {
        public void Configure(EntityTypeBuilder<AttributeSetting> builder)
        {
            builder.ToTable("Attribute", "SET");

            builder.HasKey(e => e.Id).HasName("PK_Attribute");

            builder.Property(e => e.Id).HasColumnName("AttributeId").IsRequired().HasColumnType(SqlDataType.Int32).UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType(SqlDataType.Code);
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType(SqlDataType.Name);
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType(SqlDataType.Description);
            builder.Property(e => e.ScopeType).HasColumnName("ScopeType").IsRequired().HasColumnType(SqlDataType.VarChar25);
            builder.Property(e => e.DataType).HasColumnName("DataType").IsRequired().HasColumnType(SqlDataType.DataType);
            builder.Property(e => e.Configuration).HasColumnName("Configuration").IsRequired().HasColumnType(SqlDataType.Xml);
            builder.Property(e => e.IsVisible).HasColumnName("IsVisible").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType(SqlDataType.Boolean);

            builder.HasIndex(e => new { e.Code, e.ScopeType }).IsUnique().HasDatabaseName("UK_Attribute");
        }
    }
}