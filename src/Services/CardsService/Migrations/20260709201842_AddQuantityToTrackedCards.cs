using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsService.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityToTrackedCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TrackedCards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TrackedCards");
        }
    }
}
