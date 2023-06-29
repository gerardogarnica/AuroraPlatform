using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class CredentialLogConfiguration : IEntityTypeConfiguration<CredentialLog>
    {
        public void Configure(EntityTypeBuilder<CredentialLog> builder)
        {
            builder.ToTable("CredentialLog", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_CredentialLog");

            builder.Property(e => e.Id).HasColumnName("LogId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.PasswordControl).HasColumnName("PasswordControl").IsRequired().HasColumnType("varchar(1000)");
            builder.Property(e => e.ChangeVersion).HasColumnName("ChangeVersion").IsRequired().HasColumnType("int");
            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate").HasColumnType("datetime");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.EndDate).HasColumnName("EndDate").HasColumnType("datetime");
        }
    }
}