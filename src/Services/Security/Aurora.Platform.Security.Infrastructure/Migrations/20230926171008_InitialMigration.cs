using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aurora.Platform.Security.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SEC");

            migrationBuilder.CreateTable(
                name: "CredentialLog",
                schema: "SEC",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "varchar(200)", nullable: false),
                    PasswordControl = table.Column<string>(type: "varchar(1000)", nullable: false),
                    ChangeVersion = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialLog", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "SEC",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    AppCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    AppName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    RoleGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newId()"),
                    Notes = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "SEC",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newId()"),
                    Password = table.Column<string>(type: "varchar(200)", nullable: false),
                    PasswordControl = table.Column<string>(type: "varchar(1000)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserSession",
                schema: "SEC",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Application = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    AccessToken = table.Column<string>(type: "varchar(4000)", nullable: false),
                    AccessTokenExpiration = table.Column<DateTime>(type: "datetime", nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(200)", nullable: false),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime", nullable: false),
                    BeginSessionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndSessionDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSession", x => x.SessionId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "SEC",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRole_Role",
                        column: x => x.RoleId,
                        principalSchema: "SEC",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User",
                        column: x => x.UserId,
                        principalSchema: "SEC",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "SEC",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Application = table.Column<string>(type: "varchar(50)", nullable: false),
                    AccessToken = table.Column<string>(type: "varchar(4000)", nullable: true),
                    AccessTokenExpiration = table.Column<DateTime>(type: "datetime", nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(200)", nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime", nullable: true),
                    IssuedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_UserToken_User",
                        column: x => x.UserId,
                        principalSchema: "SEC",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UK_Role",
                schema: "SEC",
                table: "Role",
                columns: new[] { "Name", "AppCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_User",
                schema: "SEC",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "SEC",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UK_UserRole",
                schema: "SEC",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_UserToken",
                schema: "SEC",
                table: "UserToken",
                columns: new[] { "UserId", "Application" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CredentialLog",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "UserSession",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "SEC");

            migrationBuilder.DropTable(
                name: "User",
                schema: "SEC");
        }
    }
}
