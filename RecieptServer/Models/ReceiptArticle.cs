using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReceiptServer.Models
{
    // Join entity representing a line on a receipt
    public class ReceiptArticle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        [Required]
        public int ArticleId { get; set; }
        [Required]
        public Article Article { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal Total { get; set; }
    }
}