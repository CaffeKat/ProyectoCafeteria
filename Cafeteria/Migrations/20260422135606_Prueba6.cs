using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafeteria.Migrations
{
    /// <inheritdoc />
    public partial class Prueba6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "porcentaje",
                table: "Descuentos",
                newName: "Porcentaje");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Descuentos",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Descuentos",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EsClientePorDefecto",
                table: "Cliente",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Cliente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimaModificacion",
                table: "Cliente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "EsClientePorDefecto",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "FechaUltimaModificacion",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "Porcentaje",
                table: "Descuentos",
                newName: "porcentaje");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Descuentos",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Descuentos",
                newName: "id");
        }
    }
}
