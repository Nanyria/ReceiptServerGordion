using System.ComponentModel.DataAnnotations;

namespace ReceiptServer.Models
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ReceiptNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public List<Article> Articles { get; set; } = new();
    }
}
