using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ReceiptServer.Models;
using ReceiptServer.Repositories;
using ReceiptServer.Services;
using RecieptServer.Models;
using Xunit;

namespace ReceiptServiceUnitTests
{
    //// Simple in-memory fake repository for unit testing ReceiptService
    //internal class FakeReceiptRepository : IReceiptServiceRepositoriy<ReceiptDTO>
    //{
    //    private readonly List<Receipt> _store = new();

    //    public Task CreateAsync(ReceiptDTO receipt)
    //    {
    //        // Simulate EF assigning an Id
    //        receipt.Id = (_store.Any() ? _store.Max(r => r.Id) : 0) + 1;
    //        _store.Add(receipt);
    //        return Task.CompletedTask;
    //    }

    //    public Task DeleteAsync(ReceiptDTO receipt)
    //    {
    //        _store.Remove(receipt);
    //        return Task.CompletedTask;
    //    }

    //    public Task<IEnumerable<ReceiptDTO>> GetAllAsync()
    //    {
    //        return Task.FromResult<IEnumerable<ReceiptDTO>>(_store);
    //    }

    //    public Task<Receipt> GetByIdAsync(int id)
    //    {
    //        return Task.FromResult(_store.FirstOrDefault(r => r.Id == id));
    //    }

    //    public Task SaveAsync()
    //    {
    //        // No-op for in-memory fake
    //        return Task.CompletedTask;
    //    }

    //    public Task UpdateAsync(ReceiptDTO receipt)
    //    {
    //        var existing = _store.FirstOrDefault(r => r.Id == receipt.Id);
    //        if (existing != null)
    //        {
    //            _store.Remove(existing);
    //            _store.Add(receipt);
    //        }
    //        return Task.CompletedTask;
    //    }

    //    // helper for assertions
    //    public IReadOnlyList<Receipt> StoredReceipts => _store;
    //}

    //public class ReceiptServiceTests
    //{
    //    [Fact]
    //    public async Task CreateReceiptAsync_ValidReceipt_ReturnsCreatedAndCalculatesTotal()
    //    {
    //        // Arrange
    //        var fakeRepo = new FakeReceiptRepository();
    //        var service = new ReceiptService(fakeRepo);

    //        var receipt = new Receipt
    //        {
    //            Date = new DateTime(2025, 12, 1),
    //            ReceiptArticles = new List<ReceiptArticle>
    //            {
    //                new ReceiptArticle { ArticleId = 1, UnitPrice = 2.50m, Quantity = 2 },
    //                new ReceiptArticle { ArticleId = 2, UnitPrice = 3.00m, Quantity = 1 }
    //            }
    //        };

    //        // expected per-line totals and overall total
    //        var expectedLineTotals = new[] { 2.50m * 2, 3.00m * 1 };
    //        var expectedTotal = expectedLineTotals.Sum();

    //        // Act
    //        var result = await service.CreateAsync(receipt);

    //        // Assert overall result
    //        Assert.True(result.IsSuccess);
    //        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    //        Assert.NotNull(result.Result);
    //        Assert.Equal(expectedTotal, result.Result.TotalAmount);

    //        // repository should have stored one receipt
    //        Assert.Single(fakeRepo.StoredReceipts);
    //        var stored = fakeRepo.StoredReceipts[0];
    //        Assert.Equal(result.Result.Id, stored.Id);
    //        Assert.Equal(expectedTotal, stored.TotalAmount);

    //        // Assert per-line totals were set on the returned receipt
    //        Assert.NotNull(result.Result.ReceiptArticles);
    //        Assert.Equal(2, result.Result.ReceiptArticles.Count);

    //        for (int i = 0; i < expectedLineTotals.Length; i++)
    //        {
    //            Assert.Equal(expectedLineTotals[i], result.Result.ReceiptArticles[i].Total);
    //        }

    //        // Assert per-line totals were persisted in the stored receipt as well
    //        Assert.NotNull(stored.ReceiptArticles);
    //        Assert.Equal(2, stored.ReceiptArticles.Count);
    //        for (int i = 0; i < expectedLineTotals.Length; i++)
    //        {
    //            Assert.Equal(expectedLineTotals[i], stored.ReceiptArticles[i].Total);
    //        }
    //    }

    //    [Fact]
    //    public async Task CreateReceiptAsync_InvalidReceipt_ReturnsBadRequest()
    //    {
    //        // Arrange
    //        var fakeRepo = new FakeReceiptRepository();
    //        var service = new ReceiptService(fakeRepo);

    //        var invalidReceipt = new Receipt
    //        {
    //            Date = DateTime.UtcNow,
    //            ReceiptArticles = new List<ReceiptArticle>() // empty list => invalid per service validation
    //        };

    //        // Act
    //        var result = await service.CreateAsync(invalidReceipt);

    //        // Assert
    //        Assert.False(result.IsSuccess);
    //        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    //        Assert.Contains("Invalid receipt data.", result.ErrorMessages);
    //        Assert.Empty(fakeRepo.StoredReceipts);
    //    }
    //}
}