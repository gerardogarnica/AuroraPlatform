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
                name: "Role",
                schema: "SEC",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    IsGlobal = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
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
                    LoginName = table.Column<string>(type: "varchar(35)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
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
                    LoginName = table.Column<string>(type: "varchar(35)", nullable: false),
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
                name: "UserCredential",
                schema: "SEC",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "varchar(200)", nullable: false),
                    PasswordControl = table.Column<string>(type: "varchar(500)", nullable: false),
                    MustChange = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredential", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCredential_User",
                        column: x => x.UserId,
                        principalSchema: "SEC",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccessToken = table.Column<string>(type: "varchar(4000)", nullable: true),
                    AccessTokenExpiration = table.Column<DateTime>(type: "datetime", nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(200)", nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime", nullable: true),
                    IssuedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserToken_User",
                        column: x => x.UserId,
                        principalSchema: "SEC",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentialLog",
                schema: "SEC",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChangeVersion = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "varchar(200)", nullable: false),
                    PasswordControl = table.Column<string>(type: "varchar(500)", nullable: false),
                    MustChange = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentialLog", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_UserCredentialLog_UserCredential",
                        column: x => x.UserId,
                        principalSchema: "SEC",
                        principalTable: "UserCredential",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UK_Role",
                schema: "SEC",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_User",
                schema: "SEC",
                table: "User",
                column: "LoginName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCredentialLog_UserId",
                schema: "SEC",
                table: "UserCredentialLog",
                column: "UserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCredentialLog",
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
                name: "UserCredential",
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
