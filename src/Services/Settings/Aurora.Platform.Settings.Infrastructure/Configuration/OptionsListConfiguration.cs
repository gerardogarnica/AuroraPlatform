using Aurora.Platform.Settings.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Settings.Infrastructure.Configuration
{
    public class OptionsListConfiguration : IEntityTypeConfiguration<OptionsList>
    {
        public void Configure(EntityTypeBuilder<OptionsList> builder)
        {
            builder.ToTable("OptionsList", "SET");

            builder.HasKey(e => e.Id).HasName("PK_OptionsList");

            builder.Property(e => e.Id).HasColumnName("OptionsId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType("varchar(40)");
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(e => e.IsVisible).HasColumnName("IsVisible").IsRequired().HasColumnType("bit");
            builder.Property(e => e.IsEditable).HasColumnName("IsEditable").IsRequired().HasColumnType("bit");

            builder.HasIndex(e => e.Code).IsUnique().HasDatabaseName("UK_OptionsList");
        }
    }
}