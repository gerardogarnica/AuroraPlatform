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

            builder.Property(e => e.Id).HasColumnName("AttributeId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(e => e.ScopeType).HasColumnName("ScopeType").IsRequired().HasColumnType("varchar(20)");
            builder.Property(e => e.DataType).HasColumnName("DataType").IsRequired().HasColumnType("varchar(10)");
            builder.Property(e => e.Configuration).HasColumnName("Configuration").IsRequired().HasColumnType("xml");
            builder.Property(e => e.IsVisible).HasColumnName("IsVisible").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");

            builder.HasIndex(e => new { e.Code, e.ScopeType }).IsUnique().HasDatabaseName("UK_Attribute");
        }
    }
}