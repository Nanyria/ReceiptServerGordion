using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecieptServer.Migrations
{
    /// <inheritdoc />
    public partial class translateToSwedish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1001,
                column: "Name",
                value: "Mjölk");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1002,
                column: "Name",
                value: "Bröd");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1003,
                column: "Name",
                value: "Ägg");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1004,
                column: "Name",
                value: "Anteckningsbok");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1001,
                column: "Name",
                value: "Milk");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1002,
                column: "Name",
                value: "Bread");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1003,
                column: "Name",
                value: "Eggs");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1004,
                column: "Name",
                value: "Notebook");
        }
    }
}
