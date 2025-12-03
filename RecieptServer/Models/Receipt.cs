using System.ComponentModel.DataAnnotations;

namespace ReceiptServer.Models
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Many-to-many via ReceiptItem (join entity)
        public List<ReceiptArticle> ReceiptArticles { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
