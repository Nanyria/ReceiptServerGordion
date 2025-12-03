using System.Collections.Generic;
using System.Threading.Tasks;
using ReceiptServer.Models;

namespace RecieptServer.Services
{
    public interface IReceiptServerService<T> where T : class
    {
        Task<APIResponse<T>> GetByIdAsync(int id);
        Task<APIResponse<T>> CreateAsync(T entity);
        Task<APIResponse<List<T>>> GetAllAsync();
        Task<APIResponse<T>> UpdateAsync(T entity);
        Task<APIResponse<T>> DeleteAsync(int id);
    }
}
