using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Settings.Infrastructure.Configuration
{
    public class OptionsCatalogItemConfiguration : IEntityTypeConfiguration<OptionsCatalogItem>
    {
        public void Configure(EntityTypeBuilder<OptionsCatalogItem> builder)
        {
            builder.ToTable("OptionsCatalogItem", "SET");

            builder.HasKey(e => e.Id).HasName("PK_OptionsCatalogItem");

            builder.Property(e => e.Id).HasColumnName("OptionsItemId").IsRequired().HasColumnType(SqlDataType.Int32).UseIdentityColumn();
            builder.Property(e => e.OptionsId).HasColumnName("OptionsId").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType(SqlDataType.Code);
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType(SqlDataType.Description);
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.AddAuditableProperties();

            builder.HasIndex(e => new { e.OptionsId, e.Code }).IsUnique().HasDatabaseName("UK_OptionsCatalogItem");

            builder.HasOne(e => e.Catalog).WithMany(e => e.Items).HasForeignKey(e => e.OptionsId).HasConstraintName("FK_OptionsCatalogItem_OptionsCatalog");
        }
    }
}