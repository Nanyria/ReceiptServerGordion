using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        // Many-to-many via ReceiptItem (join entity)
        public List<ReceiptArticle> ReceiptArticles { get; set; } = new();
    }
}

