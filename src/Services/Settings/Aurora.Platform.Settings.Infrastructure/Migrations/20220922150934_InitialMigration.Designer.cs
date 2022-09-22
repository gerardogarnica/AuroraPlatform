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
    [Migration("20220922150934_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.AttributeSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AttributeId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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
                        .HasColumnType("varchar(10)")
                        .HasColumnName("DataType");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
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
                        .HasColumnType("varchar(20)")
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
                        .HasColumnType("varchar(35)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

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

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.OptionsList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OptionsId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Description");

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

                    b.HasKey("Id")
                        .HasName("PK_OptionsList");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("UK_OptionsList");

                    b.ToTable("OptionsList", "SET");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.OptionsListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OptionsItemId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasColumnName("Code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsEditable")
                        .HasColumnType("bit")
                        .HasColumnName("IsEditable");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(35)")
                        .HasColumnName("LastUpdatedBy");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("LastUpdatedDate");

                    b.Property<int>("OptionsId")
                        .HasColumnType("int")
                        .HasColumnName("OptionsId");

                    b.HasKey("Id")
                        .HasName("PK_OptionsListItem");

                    b.HasIndex("OptionsId", "Code")
                        .IsUnique()
                        .HasDatabaseName("UK_OptionsListItem");

                    b.ToTable("OptionsListItem", "SET");
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

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.OptionsListItem", b =>
                {
                    b.HasOne("Aurora.Platform.Settings.Domain.Entities.OptionsList", "List")
                        .WithMany("Items")
                        .HasForeignKey("OptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_OptionsListItem_OptionsList");

                    b.Navigation("List");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.AttributeSetting", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("Aurora.Platform.Settings.Domain.Entities.OptionsList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
