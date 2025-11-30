using System.ComponentModel.DataAnnotations;

namespace ReceiptServer.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public int? ReceiptId { get; set; }
        public Receipt? Receipt { get; set; }

    }
}

