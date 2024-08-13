using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lonches_Restaurant.Migrations
{
    /// <inheritdoc />
    public partial class ultimaahorasi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "comanda",
                newName: "id_cliente");

            migrationBuilder.RenameIndex(
                name: "IX_comanda_IdCliente",
                table: "comanda",
                newName: "IX_comanda_id_cliente");

            migrationBuilder.AddColumn<int>(
                name: "id_empleado",
                table: "comanda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_comanda_id_empleado",
                table: "comanda",
                column: "id_empleado");

            migrationBuilder.AddForeignKey(
                name: "fk_id_comanda_empleado",
                table: "comanda",
                column: "id_empleado",
                principalTable: "empleado",
                principalColumn: "id_empleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_comanda_empleado",
                table: "comanda");

            migrationBuilder.DropIndex(
                name: "IX_comanda_id_empleado",
                table: "comanda");

            migrationBuilder.DropColumn(
                name: "id_empleado",
                table: "comanda");

            migrationBuilder.RenameColumn(
                name: "id_cliente",
                table: "comanda",
                newName: "IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_comanda_id_cliente",
                table: "comanda",
                newName: "IX_comanda_IdCliente");
        }
    }
}
