using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL_MEDITERRANEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Hotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    NumeroRecibo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: false),
                    FacturaModelNumeroFactura = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.NumeroRecibo);
                    table.ForeignKey(
                        name: "FK_Venta_Factura_FacturaModelNumeroFactura",
                        column: x => x.FacturaModelNumeroFactura,
                        principalTable: "Factura",
                        principalColumn: "NumeroFactura",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factura_HotelModelNombreHotel",
                table: "Factura",
                column: "HotelModelNombreHotel");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_FacturaModelNumeroFactura",
                table: "Venta",
                column: "FacturaModelNumeroFactura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Hotel");
        }
    }
}
