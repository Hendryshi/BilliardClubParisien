using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BilliardClubParisien");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "BilliardClubParisien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "BilliardClubParisien",
                newName: "Users");
        }
    }
}
