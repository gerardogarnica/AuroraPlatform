﻿// <auto-generated />
using System;
using Aurora.Platform.Security.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aurora.Platform.Security.Infrastructure.Migrations
{
    [DbContext(typeof(SecurityContext))]
    partial class SecurityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.CredentialLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LogId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChangeVersion")
                        .HasColumnType("int")
                        .HasColumnName("ChangeVersion");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime")
                        .HasColumnName("EndDate");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("ExpirationDate");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Password");

                    b.Property<string>("PasswordControl")
                        .IsRequired()
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("PasswordControl");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id")
                        .HasName("PK_CredentialLog");

                    b.ToTable("CredentialLog", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AppCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("AppCode");

                    b.Property<string>("AppName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("AppName");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleGuid")
                        .HasDefaultValueSql("newId()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("Notes");

                    b.HasKey("Id")
                        .HasName("PK_Role");

                    b.HasIndex("Name", "AppCode")
                        .IsUnique()
                        .HasDatabaseName("UK_Role");

                    b.ToTable("Role", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("FirstName");

                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserGuid")
                        .HasDefaultValueSql("newId()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit")
                        .HasColumnName("IsDefault");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("LastName");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("Notes");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Password");

                    b.Property<string>("PasswordControl")
                        .IsRequired()
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("PasswordControl");

                    b.Property<DateTime?>("PasswordExpirationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("ExpirationDate");

                    b.HasKey("Id")
                        .HasName("PK_User");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("UK_User");

                    b.ToTable("User", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserRoleId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit")
                        .HasColumnName("IsDefault");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id")
                        .HasName("PK_UserRole");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique()
                        .HasDatabaseName("UK_UserRole");

                    b.ToTable("UserRole", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.UserSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SessionId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("varchar(4000)")
                        .HasColumnName("AccessToken");

                    b.Property<DateTime>("AccessTokenExpiration")
                        .HasColumnType("datetime")
                        .HasColumnName("AccessTokenExpiration");

                    b.Property<string>("Application")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Application");

                    b.Property<DateTime>("BeginSessionDate")
                        .HasColumnType("datetime")
                        .HasColumnName("BeginSessionDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email");

                    b.Property<DateTime?>("EndSessionDate")
                        .HasColumnType("datetime")
                        .HasColumnName("EndSessionDate");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("RefreshToken");

                    b.Property<DateTime>("RefreshTokenExpiration")
                        .HasColumnType("datetime")
                        .HasColumnName("RefreshTokenExpiration");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id")
                        .HasName("PK_UserSession");

                    b.ToTable("UserSession", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TokenId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .HasColumnType("varchar(4000)")
                        .HasColumnName("AccessToken");

                    b.Property<DateTime?>("AccessTokenExpiration")
                        .HasColumnType("datetime")
                        .HasColumnName("AccessTokenExpiration");

                    b.Property<string>("Application")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Application");

                    b.Property<DateTime?>("IssuedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("IssuedDate");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("RefreshToken");

                    b.Property<DateTime?>("RefreshTokenExpiration")
                        .HasColumnType("datetime")
                        .HasColumnName("RefreshTokenExpiration");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id")
                        .HasName("PK_UserToken");

                    b.HasIndex("UserId", "Application")
                        .IsUnique()
                        .HasDatabaseName("UK_UserToken");

                    b.ToTable("UserToken", "SEC");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Aurora.Platform.Security.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRole_Role");

                    b.HasOne("Aurora.Platform.Security.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRole_User");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.UserToken", b =>
                {
                    b.HasOne("Aurora.Platform.Security.Domain.Entities.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserToken_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Aurora.Platform.Security.Domain.Entities.User", b =>
                {
                    b.Navigation("Tokens");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}