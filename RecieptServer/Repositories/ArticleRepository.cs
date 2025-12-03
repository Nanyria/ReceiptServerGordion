using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReceiptServer.Models;
using ReceiptServer.Data;

namespace ReceiptServer.Repositories
{
    public class ArticleRepository : IReceiptServiceRepositoriy<Article>
    {
        private readonly AppDbContext _context;
        public ArticleRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Article>> GetAllAsync()
            => await _context.Articles
                             .Include(r => r.ReceiptArticles)
                             .ToListAsync();

        public async Task<Article?> GetByIdAsync(int id)
            => await _context.Articles
                             .Include(r => r.ReceiptArticles)
                             .FirstOrDefaultAsync(r => r.Id == id);

        public async Task CreateAsync(Article entity)
        {
            await _context.Articles.AddAsync(entity);
            await SaveAsync();
        }

        public async Task UpdateAsync(Article entity)
        {
            _context.Articles.Update(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(Article entity)
        {
            _context.Articles.Remove(entity);
            await SaveAsync();
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
