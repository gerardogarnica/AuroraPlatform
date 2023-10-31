using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("Application", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_Application");

            builder.Property(e => e.Id).HasColumnName("ApplicationId").IsRequired().HasColumnType(SqlDataType.Int32);
            builder.Property(e => e.Code).HasColumnName("Code").IsRequired().HasColumnType(SqlDataType.Code);
            builder.Property(e => e.Name).HasColumnName("Name").IsRequired().HasColumnType(SqlDataType.Name);
            builder.Property(e => e.Description).HasColumnName("Description").HasColumnType(SqlDataType.Description);
            builder.Property(e => e.HasCustomConfig).HasColumnName("HasCustomConfig").IsRequired().HasColumnType(SqlDataType.Boolean);

            builder.HasIndex(e => e.Code).IsUnique().HasDatabaseName("UK_Application");
        }
    }
}