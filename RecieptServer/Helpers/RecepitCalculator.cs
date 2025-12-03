using ReceiptServer.Models;

namespace ReceiptServer.Helpers
{
    public class RecepitCalculator
    {
        public static decimal CalculateTotalReceiptAmount(List<ReceiptArticle> receiptItems)
        {
            decimal total = 0m;
            foreach (var item in receiptItems)
            {
                total += item.Total;
            }
            return total;
        }
        public static decimal CalculateTotalArticleAmount(ReceiptArticle receiptArticle)
        {
            receiptArticle.Total = receiptArticle.UnitPrice * receiptArticle.Quantity;
            return receiptArticle.Total;
        }
    }
}
