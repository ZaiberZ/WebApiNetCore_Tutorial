using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class LlenarVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "CreateDate", "Detalle", "ImagenURL", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 2, 8, 17, 30, 46, 615, DateTimeKind.Local).AddTicks(7360), "Rustica", "", 50, "Aldea", 5, 200.0, new DateTime(2024, 2, 8, 17, 30, 46, 615, DateTimeKind.Local).AddTicks(7376) },
                    { 2, "", new DateTime(2024, 2, 8, 17, 30, 46, 615, DateTimeKind.Local).AddTicks(7379), "Alto", "", 500, "Pueblo", 1, 20.0, new DateTime(2024, 2, 8, 17, 30, 46, 615, DateTimeKind.Local).AddTicks(7379) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
