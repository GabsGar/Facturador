using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturador.Data.Migrations
{
    public partial class condicionesPagoFromFacturaToVenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondicionesDePago",
                table: "Facturas");

            migrationBuilder.AddColumn<string>(
                name: "CondicionesDePago",
                table: "Ventas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondicionesDePago",
                table: "Ventas");

            migrationBuilder.AddColumn<string>(
                name: "CondicionesDePago",
                table: "Facturas",
                nullable: true);
        }
    }
}
