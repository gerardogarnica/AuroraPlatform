﻿// <auto-generated />
using System;
using Aurora.Platform.Settings.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aurora.Platform.Settings.Infrastructure.Migrations
{
    [DbContext(typeof(SettingsContext))]
    [Migration("20231024201955_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.AttributeSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AttributeId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Code");

                    b.Property<string>("Configuration")
                        .IsRequired()
                        .HasColumnType("xml")
                        .HasColumnName("Configuration");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasColumnName("DataType");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsEditable")
                        .HasColumnType("bit")
                        .HasColumnName("IsEditable");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit")
                        .HasColumnName("IsVisible");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.Property<string>("ScopeType")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasColumnName("ScopeType");

                    b.HasKey("Id")
                        .HasName("PK_Attribute");

                    b.HasIndex("Code", "ScopeType")
                        .IsUnique()
                        .HasDatabaseName("UK_Attribute");

                    b.ToTable("Attribute", "SET");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.AttributeValue", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("AttributeId");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

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

                    b.Property<int>("RelationshipId")
                        .HasColumnType("int")
                        .HasColumnName("RelationshipId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("xml")
                        .HasColumnName("Value");

                    b.HasKey("Id")
                        .HasName("PK_AttributeValue");

                    b.HasIndex("Id", "RelationshipId")
                        .IsUnique()
                        .HasDatabaseName("UK_AttributeValue");

                    b.ToTable("AttributeValue", "SET");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.OptionsCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OptionsId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AppCode")
                        .HasColumnType("varchar(40)")
                        .HasColumnName("AppCode");

                    b.Property<string>("AppName")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("AppName");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Code");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsEditable")
                        .HasColumnType("bit")
                        .HasColumnName("IsEditable");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("bit")
                        .HasColumnName("IsGlobal");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit")
                        .HasColumnName("IsVisible");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("PK_OptionsCatalog");

                    b.HasIndex("Code", "AppCode")
                        .IsUnique()
                        .HasDatabaseName("UK_OptionsCatalog")
                        .HasFilter("[AppCode] IS NOT NULL");

                    b.ToTable("OptionsCatalog", "SET");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.OptionsCatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OptionsItemId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsEditable")
                        .HasColumnType("bit")
                        .HasColumnName("IsEditable");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<int>("OptionsId")
                        .HasColumnType("int")
                        .HasColumnName("OptionsId");

                    b.HasKey("Id")
                        .HasName("PK_OptionsCatalogItem");

                    b.HasIndex("OptionsId", "Code")
                        .IsUnique()
                        .HasDatabaseName("UK_OptionsCatalogItem");

                    b.ToTable("OptionsCatalogItem", "SET");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.AttributeValue", b =>
                {
                    b.HasOne("Aurora.Platform.Settings.Domain.Entities.AttributeSetting", "AttributeSetting")
                        .WithMany("Values")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AttributeValue_Attribute");

                    b.Navigation("AttributeSetting");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.OptionsCatalogItem", b =>
                {
                    b.HasOne("Aurora.Platform.Settings.Domain.Entities.OptionsCatalog", "Catalog")
                        .WithMany("Items")
                        .HasForeignKey("OptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_OptionsCatalogItem_OptionsCatalog");

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.AttributeSetting", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.OptionsCatalog", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
