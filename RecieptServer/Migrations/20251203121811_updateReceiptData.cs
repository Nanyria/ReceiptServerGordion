using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecieptServer.Migrations
{
    /// <inheritdoc />
    public partial class updateReceiptData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Receipts_ReceiptId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ReceiptId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ReceiptNumber",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "Articles");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Receipts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ReceiptArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptArticles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptArticles_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ReceiptArticles",
                columns: new[] { "Id", "ArticleId", "Quantity", "ReceiptId", "Total", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 1, 0m, 1.99m },
                    { 2, 2, 1, 1, 0m, 2.49m },
                    { 3, 3, 1, 2, 0m, 3.50m },
                    { 4, 4, 3, 2, 0m, 5.00m }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptArticles_ArticleId",
                table: "ReceiptArticles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptArticles_ReceiptId",
                table: "ReceiptArticles",
                column: "ReceiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptArticles");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Receipts");

            migrationBuilder.AddColumn<int>(
                name: "ReceiptNumber",
                table: "Receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceiptId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReceiptId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReceiptId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReceiptId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReceiptId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReceiptNumber",
                value: 1001);

            migrationBuilder.UpdateData(
                table: "Receipts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReceiptNumber",
                value: 1002);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ReceiptId",
                table: "Articles",
                column: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Receipts_ReceiptId",
                table: "Articles",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id");
        }
    }
}
