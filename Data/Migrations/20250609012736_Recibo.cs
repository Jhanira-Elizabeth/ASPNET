using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL_MEDITERRANEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Recibo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReciboPDF",
                table: "Venta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroRecibo",
                table: "Gasto",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_NumeroRecibo",
                table: "Gasto",
                column: "NumeroRecibo");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Venta_NumeroRecibo",
                table: "Gasto",
                column: "NumeroRecibo",
                principalTable: "Venta",
                principalColumn: "NumeroRecibo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Venta_NumeroRecibo",
                table: "Gasto");

            migrationBuilder.DropIndex(
                name: "IX_Gasto_NumeroRecibo",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "ReciboPDF",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "NumeroRecibo",
                table: "Gasto");
        }
    }
}
