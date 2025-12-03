using ReceiptServer.Models;
using System.ComponentModel.DataAnnotations;

namespace RecieptServer.Models
{
    public class ArticleDTO
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

    }
}
