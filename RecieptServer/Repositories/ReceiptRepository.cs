using Azure;
using Microsoft.EntityFrameworkCore;
using ReceiptServer.Data;
using ReceiptServer.Models;

namespace ReceiptServer.Repositories
{
    public class ReceiptRepository : IReceiptRepositoriy
    {
        private readonly AppDbContext _dbContext;
        public ReceiptRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateReceiptAsync(Receipt receipt)
        {
            await _dbContext.Receipts.AddAsync(receipt);
        }

        public  Task DeleteReceiptAsync(Receipt receipt)
        {
            _dbContext.Receipts.Remove(receipt);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Receipt>> GetAllReceiptsAsync()
        {
            return await _dbContext.Receipts
                .Include(r => r.Articles).ToListAsync();
        }

        public async Task<Receipt> GetReceiptByIdAsync(int id)
        {
            return await _dbContext.Receipts.Include(r => r.Articles).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public  Task UpdateReceiptAsync(Receipt receipt)
        {
            _dbContext.Receipts.Update(receipt);
            return Task.CompletedTask;
        }
    }
}
