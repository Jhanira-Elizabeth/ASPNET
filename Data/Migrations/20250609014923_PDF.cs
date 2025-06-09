using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL_MEDITERRANEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class PDF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReciboPDF",
                table: "Venta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReciboPDF",
                table: "Venta",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
