using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Platform.Settings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SET");

            migrationBuilder.CreateTable(
                name: "Attribute",
                schema: "SET",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(40)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    ScopeType = table.Column<string>(type: "varchar(25)", nullable: false),
                    DataType = table.Column<string>(type: "varchar(15)", nullable: false),
                    Configuration = table.Column<string>(type: "xml", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.AttributeId);
                });

            migrationBuilder.CreateTable(
                name: "OptionsCatalog",
                schema: "SET",
                columns: table => new
                {
                    OptionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(40)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsCatalog", x => x.OptionsId);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValue",
                schema: "SET",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    RelationshipId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "xml", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValue", x => x.AttributeId);
                    table.ForeignKey(
                        name: "FK_AttributeValue_Attribute",
                        column: x => x.AttributeId,
                        principalSchema: "SET",
                        principalTable: "Attribute",
                        principalColumn: "AttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionsCatalogItem",
                schema: "SET",
                columns: table => new
                {
                    OptionsItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionsId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(40)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionsCatalogItem", x => x.OptionsItemId);
                    table.ForeignKey(
                        name: "FK_OptionsCatalogItem_OptionsCatalog",
                        column: x => x.OptionsId,
                        principalSchema: "SET",
                        principalTable: "OptionsCatalog",
                        principalColumn: "OptionsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UK_Attribute",
                schema: "SET",
                table: "Attribute",
                columns: new[] { "Code", "ScopeType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_AttributeValue",
                schema: "SET",
                table: "AttributeValue",
                columns: new[] { "AttributeId", "RelationshipId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_OptionsCatalog",
                schema: "SET",
                table: "OptionsCatalog",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_OptionsCatalogItem",
                schema: "SET",
                table: "OptionsCatalogItem",
                columns: new[] { "OptionsId", "Code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeValue",
                schema: "SET");

            migrationBuilder.DropTable(
                name: "OptionsCatalogItem",
                schema: "SET");

            migrationBuilder.DropTable(
                name: "Attribute",
                schema: "SET");

            migrationBuilder.DropTable(
                name: "OptionsCatalog",
                schema: "SET");
        }
    }
}
