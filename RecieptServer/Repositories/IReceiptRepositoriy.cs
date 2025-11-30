using ReceiptServer.Models;

namespace ReceiptServer.Repositories
{
    public interface IReceiptRepositoriy
    {
        Task<IEnumerable<Receipt>> GetAllReceiptsAsync();
        Task<Receipt> GetReceiptByIdAsync(int id);
        Task CreateReceiptAsync(Receipt receipt);
        Task UpdateReceiptAsync(Receipt receipt);
        Task DeleteReceiptAsync(Receipt receipt);
        Task SaveAsync();
    }
}
