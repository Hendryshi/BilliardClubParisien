using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "BilliardClubParisien",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                schema: "dbo",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                schema: "dbo",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                schema: "dbo",
                table: "Users");

            migrationBuilder.EnsureSchema(
                name: "BilliardClubParisien");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "Users",
                newSchema: "BilliardClubParisien");
        }
    }
}
