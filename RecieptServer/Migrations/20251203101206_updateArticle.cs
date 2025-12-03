using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecieptServer.Migrations
{
    /// <inheritdoc />
    public partial class updateArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Receipts_ReceiptId",
                table: "Articles");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Receipts_ReceiptId",
                table: "Articles",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Receipts_ReceiptId",
                table: "Articles");

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReceiptId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReceiptId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReceiptId",
                value: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Receipts_ReceiptId",
                table: "Articles",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
