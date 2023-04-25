using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Ayanet_2.Migrations
{
    /// <inheritdoc />
    public partial class CreationDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    cod_cliente = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pass_cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cod_postal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    localidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    provincia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bloqueado = table.Column<bool>(type: "bit", nullable: false),
                    fecha_export = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.cod_cliente);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    cod_producto = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    formato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipo_formato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    naturaleza = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bloqueado = table.Column<bool>(type: "bit", nullable: false),
                    cod_seguimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    calculo_caducidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha_export = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tipo_materia_fabrica = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.cod_producto);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    cod_direccion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha_export = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cod_cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientecod_cliente = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.cod_direccion);
                    table.ForeignKey(
                        name: "FK_Direcciones_Clientes_clientecod_cliente",
                        column: x => x.clientecod_cliente,
                        principalTable: "Clientes",
                        principalColumn: "cod_cliente");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_clientecod_cliente",
                table: "Direcciones",
                column: "clientecod_cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
