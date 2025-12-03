using ReceiptServer.Models;
using System.ComponentModel.DataAnnotations;

namespace RecieptServer.Models
{
    public class ReceiptArticleDTO
    {
        public int ArticleId { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; } = 1;

        public decimal UnitPrice { get; set; }

        public decimal Total { get; set; }
    }
}