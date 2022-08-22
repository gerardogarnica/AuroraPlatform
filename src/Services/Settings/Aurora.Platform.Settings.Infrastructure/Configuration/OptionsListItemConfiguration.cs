using Aurora.Platform.Settings.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Settings.Infrastructure.Configuration
{
    public class OptionsListItemConfiguration : IEntityTypeConfiguration<OptionsListItem>
    {
        public void Configure(EntityTypeBuilder<OptionsListItem> builder)
        {
            builder.ToTable("OptionsListItem", "SET");

            builder.HasKey(e => e.Id).HasName("PK_OptionsListItem");

            builder.Property(e => e.Id).HasColumnName("OptionsItemId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.OptionsId).HasColumnName("OptionsId").IsRequired().HasColumnType("int");
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType("varchar(35)");
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType("datetime");

            builder.HasIndex(e => new { e.OptionsId, e.Code }).IsUnique().HasDatabaseName("UK_OptionsListItem");

            builder.HasOne(e => e.List).WithMany(e => e.Items).HasForeignKey(e => e.OptionsId).HasConstraintName("FK_OptionsListItem_OptionsList");
        }
    }
}