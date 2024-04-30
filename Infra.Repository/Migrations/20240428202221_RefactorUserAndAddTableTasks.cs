using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUserAndAddTableTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    IdTask = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserEmail = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.IdTask);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserEmail",
                table: "Tasks",
                column: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }
    }
}
