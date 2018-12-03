using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturador.Data.Migrations
{
    public partial class addedFormaPagoToVentas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormaPagoId",
                table: "Ventas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_FormaPagoId",
                table: "Ventas",
                column: "FormaPagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_FormasPago_FormaPagoId",
                table: "Ventas",
                column: "FormaPagoId",
                principalTable: "FormasPago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_FormasPago_FormaPagoId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_FormaPagoId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "FormaPagoId",
                table: "Ventas");
        }
    }
}
