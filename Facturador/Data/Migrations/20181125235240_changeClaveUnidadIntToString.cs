using Microsoft.EntityFrameworkCore.Migrations;

namespace Facturador.Data.Migrations
{
    public partial class changeClaveUnidadIntToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Clave",
                table: "ClaveUnidad",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Clave",
                table: "ClaveUnidad",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
