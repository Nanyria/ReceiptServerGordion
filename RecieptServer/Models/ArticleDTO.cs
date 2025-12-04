using ReceiptServer.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecieptServer.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<ReceiptArticleDTO> ReceiptArticles { get; set; } = new();
    }
    public class ArticleCreateDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
