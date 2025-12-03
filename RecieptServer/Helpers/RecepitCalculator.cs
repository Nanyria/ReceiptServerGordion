using ReceiptServer.Models;
using RecieptServer.Models;

namespace ReceiptServer.Helpers
{
    public class RecepitCalculator
    {
        public static decimal CalculateTotalReceiptAmount(List<ReceiptArticleDTO> receiptItemsDTO)
        {
            decimal total = 0m;
            foreach (var item in receiptItemsDTO)
            {
                total += item.Total;
            }
            return total;
        }
        public static decimal CalculateTotalArticleAmount(ReceiptArticleDTO receiptArticleDTO)
        {
            receiptArticleDTO.Total = receiptArticleDTO.UnitPrice * receiptArticleDTO.Quantity;
            return receiptArticleDTO.Total;
        }
    }
}
