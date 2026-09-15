using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHHManager.Migrations
{
    /// <inheritdoc />
    public partial class AgregarExportaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exportaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NombreReporte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Formato = table.Column<int>(type: "int", nullable: false),
                    FechaExportacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RutaArchivo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exportaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exportaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exportaciones_UsuarioId",
                table: "Exportaciones",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exportaciones");
        }
    }
}
