using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecieptServer.Migrations
{
    /// <inheritdoc />
    public partial class updatedExemples : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1001, "Milk", 22.99m },
                    { 1002, "Bread", 38.49m },
                    { 1003, "Eggs", 37.50m },
                    { 1004, "Notebook", 85.00m }
                });

            // Insert all receipts that will be referenced by ReceiptArticles
            migrationBuilder.InsertData(
                table: "Receipts",
                columns: new[] { "Id", "Date", "TotalAmount" },
                values: new object[,]
                {
                    { 1001, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 84.47m },
                    { 1002, new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 292.50m },
                    { 1003, new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 22.99m },
                    { 1004, new DateTime(2025, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 76.98m },
                    { 1005, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 150.00m },
                    { 1006, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 85.00m },
                    { 1007, new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 38.49m },
                    { 1008, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 68.97m },
                    { 1009, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 170.00m }
                });

            migrationBuilder.InsertData(
                table: "ReceiptArticles",
                columns: new[] { "Id", "ArticleId", "Quantity", "ReceiptId", "Total", "UnitPrice" },
                values: new object[,]
                {
                    { 1001, 1001, 2, 1001, 45.98m, 22.99m },
                    { 1002, 1002, 1, 1001, 38.49m, 38.49m },
                    { 1003, 1003, 1, 1002, 37.50m, 37.50m },
                    { 1004, 1004, 3, 1002, 255.00m, 85.00m },
                    { 1005, 1001, 1, 1003, 22.99m, 22.99m },
                    { 1006, 1002, 2, 1004, 76.98m, 38.49m },
                    { 1007, 1003, 4, 1005, 150.00m, 37.50m },
                    { 1008, 1004, 1, 1006, 85.00m, 85.00m },
                    { 1009, 1002, 1, 1007, 38.49m, 38.49m },
                    { 1010, 1001, 3, 1008, 68.97m, 22.99m },
                    { 1011, 1004, 2, 1009, 170.00m, 85.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete inserted receipt articles
            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1009);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1010);

            migrationBuilder.DeleteData(
                table: "ReceiptArticles",
                keyColumn: "Id",
                keyValue: 1011);

            // Delete inserted articles
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1004);

            // Delete inserted receipts (1001..1009)
            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1006);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1007);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1008);

            migrationBuilder.DeleteData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1009);

            // Re-insert original small-id seed data (as before)
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Milk", 1.99m },
                    { 2, "Bread", 2.49m },
                    { 3, "Eggs", 3.50m },
                    { 4, "Notebook", 5.00m }
                });

            migrationBuilder.InsertData(
                table: "Receipts",
                columns: new[] { "Id", "Date", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6.47m },
                    { 2, new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 18.50m }
                });

            migrationBuilder.InsertData(
                table: "ReceiptArticles",
                columns: new[] { "Id", "ArticleId", "Quantity", "ReceiptId", "Total", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 1, 3.98m, 1.99m },
                    { 2, 2, 1, 1, 2.49m, 2.49m },
                    { 3, 3, 1, 2, 3.50m, 3.50m },
                    { 4, 4, 3, 2, 15.00m, 5.00m }
                });
        }
    }
}
