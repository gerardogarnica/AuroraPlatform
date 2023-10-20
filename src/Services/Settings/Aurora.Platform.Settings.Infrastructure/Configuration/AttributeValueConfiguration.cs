using Aurora.Framework.Repositories;
using Aurora.Platform.Settings.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Settings.Infrastructure.Configuration
{
    public class AttributeValueConfiguration : IEntityTypeConfiguration<AttributeValue>
    {
        public void Configure(EntityTypeBuilder<AttributeValue> builder)
        {
            builder.ToTable("AttributeValue", "SET");

            builder.HasKey(e => e.Id).HasName("PK_AttributeValue");

            builder.Property(e => e.Id).HasColumnName("AttributeId").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.RelationshipId).HasColumnName("RelationshipId").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.Value).HasColumnName("Value").IsRequired().HasColumnType(SqlDataType.Xml);
            builder.Property(e => e.Notes).HasColumnName("Notes").HasColumnType(SqlDataType.Notes);
            builder.AddAuditableProperties();

            builder.HasIndex(e => new { e.Id, e.RelationshipId }).IsUnique().HasDatabaseName("UK_AttributeValue");

            builder.HasOne(e => e.AttributeSetting).WithMany(e => e.Values).HasForeignKey(e => e.Id).HasConstraintName("FK_AttributeValue_Attribute");
        }
    }
}