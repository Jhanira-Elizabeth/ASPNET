using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEL_MEDITERRANEO.Data.Migrations
{
    /// <inheritdoc />
    public partial class Gastos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_Venta_NumeroRecibo",
                table: "Gasto");

            migrationBuilder.DropIndex(
                name: "IX_Gasto_NumeroRecibo",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "NumeroRecibo",
                table: "Gasto");

            migrationBuilder.RenameColumn(
                name: "IdGasto",
                table: "Gasto",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Gasto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Gasto");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Gasto",
                newName: "IdGasto");

            migrationBuilder.AddColumn<string>(
                name: "NumeroRecibo",
                table: "Gasto",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_NumeroRecibo",
                table: "Gasto",
                column: "NumeroRecibo");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_Venta_NumeroRecibo",
                table: "Gasto",
                column: "NumeroRecibo",
                principalTable: "Venta",
                principalColumn: "NumeroRecibo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
