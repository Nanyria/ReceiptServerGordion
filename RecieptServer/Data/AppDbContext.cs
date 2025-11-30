using Microsoft.EntityFrameworkCore;
using ReceiptServer.Models;
using System;

namespace ReceiptServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many from Receipt side (equivalent to configuring from Article)
            modelBuilder.Entity<Receipt>()
                .HasMany(r => r.Articles)
                .WithOne(a => a.Receipt)
                .HasForeignKey(a => a.ReceiptId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Explicitly configure decimal precision/scale for Price to avoid truncation warning.
            modelBuilder.Entity<Article>()
                .Property(a => a.Price)
                .HasPrecision(18, 2);

            // (optional) seed data — ensure ReceiptId is null or points to an existing Receipt
            modelBuilder.Entity<Receipt>().HasData(
                new Receipt { Id = 1, ReceiptNumber = 1001, Date = new DateTime(2025, 11, 01) },
                new Receipt { Id = 2, ReceiptNumber = 1002, Date = new DateTime(2025, 11, 15) }
            );

            modelBuilder.Entity<Article>().HasData(
                new Article { Id = 1, Name = "Milk", Price = 1.99m, ReceiptId = 1 },
                new Article { Id = 2, Name = "Bread", Price = 2.49m, ReceiptId = 1 },
                new Article { Id = 3, Name = "Eggs", Price = 3.50m, ReceiptId = 2 },
                new Article { Id = 4, Name = "Notebook", Price = 5.00m, ReceiptId = null }
            );
        }
    }
}
