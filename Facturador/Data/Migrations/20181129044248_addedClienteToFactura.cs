using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturador.Data.Migrations
{
    public partial class addedClienteToFactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "Facturas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ClienteID",
                table: "Facturas",
                column: "ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Clientes_ClienteID",
                table: "Facturas",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Clientes_ClienteID",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_ClienteID",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "Facturas");
        }
    }
}
