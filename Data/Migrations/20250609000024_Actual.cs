using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL_MEDITERRANEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Actual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habitacion");

            migrationBuilder.CreateTable(
                name: "Gasto",
                columns: table => new
                {
                    IdGasto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroRecibo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gasto", x => x.IdGasto);
                    table.ForeignKey(
                        name: "FK_Gasto_Venta_NumeroRecibo",
                        column: x => x.NumeroRecibo,
                        principalTable: "Venta",
                        principalColumn: "NumeroRecibo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_NumeroRecibo",
                table: "Gasto",
                column: "NumeroRecibo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gasto");

            migrationBuilder.CreateTable(
                name: "Habitacion",
                columns: table => new
                {
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstaOcupada = table.Column<bool>(type: "bit", nullable: false),
                    PrecioNoche = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoHabitacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.NumeroHabitacion);
                });
        }
    }
}
