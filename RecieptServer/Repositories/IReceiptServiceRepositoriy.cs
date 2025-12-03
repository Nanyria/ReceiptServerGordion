using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReceiptServer.Repositories
{
    public interface IReceiptServiceRepositoriy<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
