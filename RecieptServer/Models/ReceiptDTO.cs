using ReceiptServer.Models;
using System.ComponentModel.DataAnnotations;

namespace RecieptServer.Models
{
    public class ReceiptDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<ReceiptArticleDTO> ReceiptArticles { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
