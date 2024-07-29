using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "products",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 29, 7, 32, 13, 440, DateTimeKind.Utc).AddTicks(4524), new DateTime(2024, 7, 29, 7, 32, 13, 440, DateTimeKind.Utc).AddTicks(4526) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sku",
                table: "products");

            migrationBuilder.UpdateData(
                table: "administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 7, 29, 5, 49, 44, 31, DateTimeKind.Utc).AddTicks(424), new DateTime(2024, 7, 29, 5, 49, 44, 31, DateTimeKind.Utc).AddTicks(426) });
        }
    }
}
