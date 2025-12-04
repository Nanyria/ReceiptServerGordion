using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecieptServer.Migrations
{
    /// <inheritdoc />
    public partial class updateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Total",
                value: 3.98m);

            migrationBuilder.UpdateData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Total",
                value: 2.49m);

            migrationBuilder.UpdateData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Total",
                value: 3.50m);

            migrationBuilder.UpdateData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Total",
                value: 15.00m);

            migrationBuilder.UpdateData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalAmount",
                value: 6.47m);

            migrationBuilder.UpdateData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalAmount",
                value: 18.50m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Total",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Total",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Total",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Total",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalAmount",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalAmount",
                value: 0m);
        }
    }
}
