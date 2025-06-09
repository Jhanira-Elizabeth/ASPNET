using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL_MEDITERRANEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Habitaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habitacion",
                columns: table => new
                {
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstaOcupada = table.Column<bool>(type: "bit", nullable: false),
                    TipoHabitacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioNoche = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.NumeroHabitacion);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habitacion");
        }
    }
}
