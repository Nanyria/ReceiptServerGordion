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
            modelBuilder.Entity<Receipt>().HasData(
                new Receipt { Id = 1, Date = new DateTime(2025, 11, 01), TotalAmount = 0m },
                new Receipt { Id = 2, Date = new DateTime(2025, 11, 15), TotalAmount = 0m }
            );

            // Seed articles (catalog)
            modelBuilder.Entity<Article>().HasData(
                new Article { Id = 1, Name = "Milk", Price = 1.99m },
                new Article { Id = 2, Name = "Bread", Price = 2.49m },
                new Article { Id = 3, Name = "Eggs", Price = 3.50m },
                new Article { Id = 4, Name = "Notebook", Price = 5.00m }
            );

            // Seed receipt items (lines) linking receipts and articles
            modelBuilder.Entity<ReceiptArticle>().HasData(
                new ReceiptArticle { Id = 1, ReceiptId = 1, ArticleId = 1, Quantity = 2, UnitPrice = 1.99m },
                new ReceiptArticle { Id = 2, ReceiptId = 1, ArticleId = 2, Quantity = 1, UnitPrice = 2.49m },
                new ReceiptArticle { Id = 3, ReceiptId = 2, ArticleId = 3, Quantity = 1, UnitPrice = 3.50m },
                new ReceiptArticle { Id = 4, ReceiptId = 2, ArticleId = 4, Quantity = 3, UnitPrice = 5.00m }
            );
        }

        // Ensure totals are calculated before saving
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedReceipts = ChangeTracker
                .Entries<Receipt>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            foreach (var entry in changedReceipts)
            {
                var receipt = entry.Entity;
                decimal total = 0m;

                // If the receipt has its ReceiptArticles loaded (common when created/updated via API),
                // use them directly.
                if (receipt.ReceiptArticles != null && receipt.ReceiptArticles.Any())
                {
                    total = RecepitCalculator.CalculateTotalReceiptAmount(receipt.ReceiptArticles);
                }
                else if (receipt.Id != 0)
                {
                    // Otherwise load current lines from DB to compute total (useful on updates).
                    var items = await ReceiptArticles
                        .Where(ra => ra.ReceiptId == receipt.Id)
                        .ToListAsync(cancellationToken);
                    total = RecepitCalculator.CalculateTotalReceiptAmount(items);
                }

                receipt.TotalAmount = total;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        // Optional: forward synchronous calls to the async implementation
        public override int SaveChanges()
            => SaveChangesAsync().GetAwaiter().GetResult();
    }
}
