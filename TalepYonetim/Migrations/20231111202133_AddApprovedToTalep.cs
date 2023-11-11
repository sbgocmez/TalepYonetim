using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalepYonetim.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovedToTalep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Onaylandi",
                table: "Talepler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Onaylandi",
                table: "Talepler");
        }
    }
}
