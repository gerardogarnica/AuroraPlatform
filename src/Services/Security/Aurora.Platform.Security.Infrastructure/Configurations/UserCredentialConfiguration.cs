using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            builder.ToTable("UserCredential", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_UserCredential");

            builder.Property(e => e.Id).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.PasswordControl).HasColumnName("PasswordControl").IsRequired().HasColumnType("varchar(500)");
            builder.Property(e => e.MustChange).HasColumnName("MustChange").IsRequired().HasColumnType("bit");
            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate").HasColumnType("datetime");
            builder.AddAuditableProperties();

            builder.HasOne(e => e.User).WithOne(e => e.Credential).HasConstraintName("FK_UserCredential_User");
        }
    }
}