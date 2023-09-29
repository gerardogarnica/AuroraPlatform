using Aurora.Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Framework.Repositories
{
    public static class EntityConfigurationExtensions
    {
        public static void AddAuditableProperties<T>(this EntityTypeBuilder<T> builder) where T : AuditableEntity
        {
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").IsRequired().HasColumnType(SqlDataType.Email);
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType(SqlDataType.DateTime);
            builder.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy").IsRequired().HasColumnType(SqlDataType.Email);
            builder.Property(e => e.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired().HasColumnType(SqlDataType.DateTime);
        }
    }
}