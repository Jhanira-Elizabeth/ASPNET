using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL_MEDITERRANEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Actualizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Factura_FacturaModelNumeroFactura",
                table: "Venta");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Venta_FacturaModelNumeroFactura",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "FacturaModelNumeroFactura",
                table: "Venta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacturaModelNumeroFactura",
                table: "Venta",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    NombreHotel = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.NombreHotel);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    NumeroFactura = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HotelModelNombreHotel = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.NumeroFactura);
                    table.ForeignKey(
                        name: "FK_Factura_Hotel_HotelModelNombreHotel",
                        column: x => x.HotelModelNombreHotel,
                        principalTable: "Hotel",
                        principalColumn: "NombreHotel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venta_FacturaModelNumeroFactura",
                table: "Venta",
                column: "FacturaModelNumeroFactura");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_HotelModelNombreHotel",
                table: "Factura",
                column: "HotelModelNombreHotel");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Factura_FacturaModelNumeroFactura",
                table: "Venta",
                column: "FacturaModelNumeroFactura",
                principalTable: "Factura",
                principalColumn: "NumeroFactura",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
