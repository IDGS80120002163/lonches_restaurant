using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lonches_Restaurant.Migrations
{
    /// <inheritdoc />
    public partial class modificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empleado",
                columns: table => new
                {
                    id_empleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ape_paterno = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ape_materno = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    contrasenia = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    rol = table.Column<int>(type: "int", nullable: false),
                    estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__empleado__88B51394BC852F41", x => x.id_empleado);
                });

            migrationBuilder.CreateTable(
                name: "mesa",
                columns: table => new
                {
                    id_mesa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    num_mesa = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mesa__68A1E15942B7D0A4", x => x.id_mesa);
                });

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fotografia = table.Column<string>(type: "text", nullable: true),
                    precio = table.Column<double>(type: "float", nullable: false),
                    estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producto__FF341C0DF7F68424", x => x.id_producto);
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    id_mesa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cliente__677F38F5E66C4FBC", x => x.id_cliente);
                    table.ForeignKey(
                        name: "fk_id_cliente_mesa",
                        column: x => x.id_mesa,
                        principalTable: "mesa",
                        principalColumn: "id_mesa");
                });

            migrationBuilder.CreateTable(
                name: "empleado_mesa_intermedia",
                columns: table => new
                {
                    id_empleado_mesa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empleado = table.Column<int>(type: "int", nullable: false),
                    id_mesa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__empleado__E0EEFA74198E8502", x => x.id_empleado_mesa);
                    table.ForeignKey(
                        name: "fk_id_empleado_mesa",
                        column: x => x.id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id_empleado");
                    table.ForeignKey(
                        name: "fk_id_mesa_empleado",
                        column: x => x.id_mesa,
                        principalTable: "mesa",
                        principalColumn: "id_mesa");
                });

            migrationBuilder.CreateTable(
                name: "comanda",
                columns: table => new
                {
                    id_comanda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    id_mesa = table.Column<int>(type: "int", nullable: false),
                    estatus = table.Column<int>(type: "int", nullable: false),
                    ComandumIdComanda = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comanda__6D6D170D711F9C1C", x => x.id_comanda);
                    table.ForeignKey(
                        name: "FK_comanda_comanda_ComandumIdComanda",
                        column: x => x.ComandumIdComanda,
                        principalTable: "comanda",
                        principalColumn: "id_comanda");
                    table.ForeignKey(
                        name: "fk_id_comanda_cliente",
                        column: x => x.IdCliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "fk_id_comanda_mesa",
                        column: x => x.id_mesa,
                        principalTable: "mesa",
                        principalColumn: "id_mesa");
                });

            migrationBuilder.CreateTable(
                name: "venta",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empleado = table.Column<int>(type: "int", nullable: false),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_comanda = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__venta__459533BF44ED1F6A", x => x.id_venta);
                    table.ForeignKey(
                        name: "fk_id_venta_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "fk_id_venta_empleado",
                        column: x => x.id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id_empleado");
                });

            migrationBuilder.CreateTable(
                name: "comanda_detalle",
                columns: table => new
                {
                    id_comanda_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_comanda = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    cantidad_pedida = table.Column<int>(type: "int", nullable: false),
                    precio_unitario = table.Column<double>(type: "float", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comanda___BF1F6694B0FC3AB2", x => x.id_comanda_detalle);
                    table.ForeignKey(
                        name: "fk_id_comanda_comanda",
                        column: x => x.id_comanda,
                        principalTable: "comanda",
                        principalColumn: "id_comanda");
                    table.ForeignKey(
                        name: "fk_id_comanda_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto");
                });

            migrationBuilder.CreateTable(
                name: "produccion",
                columns: table => new
                {
                    id_produccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_comanda = table.Column<int>(type: "int", nullable: false),
                    id_empleado = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producci__9EBBA4335C138B7A", x => x.id_produccion);
                    table.ForeignKey(
                        name: "fk_id_produccion_comanda",
                        column: x => x.id_comanda,
                        principalTable: "comanda",
                        principalColumn: "id_comanda");
                    table.ForeignKey(
                        name: "fk_id_produccion_empleado",
                        column: x => x.id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id_empleado");
                });

            migrationBuilder.CreateTable(
                name: "venta_detalle",
                columns: table => new
                {
                    id_venta_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_venta = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    cantidad_vendida = table.Column<int>(type: "int", nullable: false),
                    precio_unitario = table.Column<double>(type: "float", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__venta_de__7AA8F41BB9F3BF5A", x => x.id_venta_detalle);
                    table.ForeignKey(
                        name: "fk_id_venta_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto");
                    table.ForeignKey(
                        name: "fk_id_venta_venta",
                        column: x => x.id_venta,
                        principalTable: "venta",
                        principalColumn: "id_venta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cliente_id_mesa",
                table: "cliente",
                column: "id_mesa");

            migrationBuilder.CreateIndex(
                name: "IX_comanda_ComandumIdComanda",
                table: "comanda",
                column: "ComandumIdComanda");

            migrationBuilder.CreateIndex(
                name: "IX_comanda_id_mesa",
                table: "comanda",
                column: "id_mesa");

            migrationBuilder.CreateIndex(
                name: "IX_comanda_IdCliente",
                table: "comanda",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_comanda_detalle_id_comanda",
                table: "comanda_detalle",
                column: "id_comanda");

            migrationBuilder.CreateIndex(
                name: "IX_comanda_detalle_id_producto",
                table: "comanda_detalle",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_mesa_intermedia_id_empleado",
                table: "empleado_mesa_intermedia",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_mesa_intermedia_id_mesa",
                table: "empleado_mesa_intermedia",
                column: "id_mesa");

            migrationBuilder.CreateIndex(
                name: "IX_produccion_id_comanda",
                table: "produccion",
                column: "id_comanda");

            migrationBuilder.CreateIndex(
                name: "IX_produccion_id_empleado",
                table: "produccion",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_venta_id_cliente",
                table: "venta",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_venta_id_empleado",
                table: "venta",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_venta_detalle_id_producto",
                table: "venta_detalle",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_venta_detalle_id_venta",
                table: "venta_detalle",
                column: "id_venta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comanda_detalle");

            migrationBuilder.DropTable(
                name: "empleado_mesa_intermedia");

            migrationBuilder.DropTable(
                name: "produccion");

            migrationBuilder.DropTable(
                name: "venta_detalle");

            migrationBuilder.DropTable(
                name: "comanda");

            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "venta");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "empleado");

            migrationBuilder.DropTable(
                name: "mesa");
        }
    }
}
