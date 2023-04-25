using Aurora.Platform.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Platform.Security.Infrastructure.Configurations
{
    public class UserCredentialLogConfiguration : IEntityTypeConfiguration<UserCredentialLog>
    {
        public void Configure(EntityTypeBuilder<UserCredentialLog> builder)
        {
            builder.ToTable("UserCredentialLog", "SEC");

            builder.HasKey(e => e.Id).HasName("PK_UserCredentialLog");

            builder.Property(e => e.Id).HasColumnName("LogId").IsRequired().HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired().HasColumnType("int");
            builder.Property(e => e.ChangeVersion).HasColumnName("ChangeVersion").IsRequired().HasColumnType("int");
            builder.Property(e => e.Password).HasColumnName("Password").IsRequired().HasColumnType("varchar(200)");
            builder.Property(e => e.PasswordControl).HasColumnName("PasswordControl").IsRequired().HasColumnType("varchar(500)");
            builder.Property(e => e.MustChange).HasColumnName("MustChange").IsRequired().HasColumnType("bit");
            builder.Property(e => e.ExpirationDate).HasColumnName("ExpirationDate").HasColumnType("datetime");
            builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired().HasColumnType("datetime");
            builder.Property(e => e.EndDate).HasColumnName("EndDate").IsRequired().HasColumnType("bit");

            builder.HasOne(e => e.Credential).WithMany(e => e.CredentialLogs).HasForeignKey(e => e.UserId).HasConstraintName("FK_UserCredentialLog_UserCredential");
        }
    }
}