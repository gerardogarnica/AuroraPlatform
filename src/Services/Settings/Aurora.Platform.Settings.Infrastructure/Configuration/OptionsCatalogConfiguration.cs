using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Settings.Infrastructure.Configuration
{
    public class OptionsCatalogConfiguration : IEntityTypeConfiguration<OptionsCatalog>
    {
        public void Configure(EntityTypeBuilder<OptionsCatalog> builder)
        {
            builder.ToTable("OptionsCatalog", "SET");

            builder.HasKey(e => e.Id).HasName("PK_OptionsCatalog");

            builder.Property(e => e.Id).HasColumnName("OptionsId").IsRequired().HasColumnType(SqlDataType.Int32).UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType(SqlDataType.Code);
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType(SqlDataType.Name);
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType(SqlDataType.Description);
            builder.Property(e => e.IsVisible).HasColumnName("IsVisible").IsRequired().HasColumnType(SqlDataType.Boolean);
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType(SqlDataType.Boolean);

            builder.HasIndex(e => e.Code).IsUnique().HasDatabaseName("UK_OptionsCatalog");
        }
    }
}