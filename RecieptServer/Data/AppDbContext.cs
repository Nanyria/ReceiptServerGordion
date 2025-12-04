using Microsoft.EntityFrameworkCore;
using ReceiptServer.Models;
using ReceiptServer.Helpers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiptServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ReceiptArticle> ReceiptArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.Property(r => r.Id)
                      .UseIdentityColumn(seed: 1001, increment: 1);

                entity.HasMany(r => r.ReceiptArticles)
                      .WithOne(ri => ri.Receipt)
                      .HasForeignKey(ri => ri.ReceiptId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(r => r.TotalAmount)
                      .HasPrecision(18, 2)
                      .HasDefaultValue(0m);
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(a => a.Id)
                      .UseIdentityColumn(seed: 1001, increment: 1);

                entity.HasMany(a => a.ReceiptArticles)
                      .WithOne(ri => ri.Article)
                      .HasForeignKey(ri => ri.ArticleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.Price)
                      .HasPrecision(18, 2);
            });

            modelBuilder.Entity<ReceiptArticle>(entity =>
            {
                entity.Property(ri => ri.UnitPrice)
                      .HasPrecision(18, 2);

                entity.Property(ra => ra.Total)
                      .HasPrecision(18, 2);
            });

            // Seed receipts (IDs and FK usage must match)
            // Totals must reflect seeded receipt items
            modelBuilder.Entity<Receipt>().HasData(
                new Receipt { Id = 1001, Date = new DateTime(2025, 11, 01), TotalAmount = 6.47m },
                new Receipt { Id = 1002, Date = new DateTime(2025, 11, 15), TotalAmount = 18.50m }
            );

            // Seed articles (catalog)
            modelBuilder.Entity<Article>().HasData(
                new Article { Id = 1001, Name = "Mjölk", Price = 22.99m },
                new Article { Id = 1002, Name = "Bröd", Price = 38.49m },
                new Article { Id = 1003, Name = "Ägg", Price = 37.50m },
                new Article { Id = 1004, Name = "Anteckningsbok", Price = 85.00m }
            );

            // Seed receipt items (lines) linking receipts and articles
            // Include Total for each line (Quantity * UnitPrice)
            modelBuilder.Entity<ReceiptArticle>().HasData(
                // existing receipts corrected to 100x IDs and article IDs
                new ReceiptArticle { Id = 1001, ReceiptId = 1001, ArticleId = 1001, Quantity = 2, UnitPrice = 22.99m, Total = 45.98m },
                new ReceiptArticle { Id = 1002, ReceiptId = 1001, ArticleId = 1002, Quantity = 1, UnitPrice = 38.49m, Total = 38.49m },
                new ReceiptArticle { Id = 1003, ReceiptId = 1002, ArticleId = 1003, Quantity = 1, UnitPrice = 37.50m, Total = 37.50m },
                new ReceiptArticle { Id = 1004, ReceiptId = 1002, ArticleId = 1004, Quantity = 3, UnitPrice = 85.00m, Total = 255.00m },

                // new receipt lines for receipts 1003..1009 (one line each)
                new ReceiptArticle { Id = 1005, ReceiptId = 1003, ArticleId = 1001, Quantity = 1, UnitPrice = 22.99m, Total = 22.99m },
                new ReceiptArticle { Id = 1006, ReceiptId = 1004, ArticleId = 1002, Quantity = 2, UnitPrice = 38.49m, Total = 76.98m },
                new ReceiptArticle { Id = 1007, ReceiptId = 1005, ArticleId = 1003, Quantity = 4, UnitPrice = 37.50m, Total = 150.00m },
                new ReceiptArticle { Id = 1008, ReceiptId = 1006, ArticleId = 1004, Quantity = 1, UnitPrice = 85.00m, Total = 85.00m },
                new ReceiptArticle { Id = 1009, ReceiptId = 1007, ArticleId = 1002, Quantity = 1, UnitPrice = 38.49m, Total = 38.49m },
                new ReceiptArticle { Id = 1010, ReceiptId = 1008, ArticleId = 1001, Quantity = 3, UnitPrice = 22.99m, Total = 68.97m },
                new ReceiptArticle { Id = 1011, ReceiptId = 1009, ArticleId = 1004, Quantity = 2, UnitPrice = 85.00m, Total = 170.00m }
            );
        }


    }
}
