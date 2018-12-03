using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturador.Data.Migrations
{
    public partial class addedVentasToTransacciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VentaId",
                table: "Transacciones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_VentaId",
                table: "Transacciones",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Ventas_VentaId",
                table: "Transacciones",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Ventas_VentaId",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_VentaId",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "VentaId",
                table: "Transacciones");
        }
    }
}
