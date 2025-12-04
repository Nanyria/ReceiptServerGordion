using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReceiptServer.Models;
using ReceiptServer.Data;

namespace ReceiptServer.Repositories
{
    public class ReceiptRepository : IReceiptServiceRepositoriy<Receipt>
    {
        private readonly AppDbContext _context;
        public ReceiptRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Receipt>> GetAllAsync()
            => await _context.Receipts
                             .Include(r => r.ReceiptArticles)
                                .ThenInclude(ra => ra.Article) 
                             .ToListAsync();

        public async Task<Receipt?> GetByIdAsync(int id)
            => await _context.Receipts
                             .Include(r => r.ReceiptArticles)
                                .ThenInclude(ra => ra.Article) 
                             .FirstOrDefaultAsync(r => r.Id == id);

        public async Task CreateAsync(Receipt entity)
        {
            await _context.Receipts.AddAsync(entity);
            await SaveAsync();
        }

        public async Task UpdateAsync(Receipt entity)
        {
            _context.Receipts.Update(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(Receipt entity)
        {
            _context.Receipts.Remove(entity);
            await SaveAsync();
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
