using ReceiptServer.Models;
using ReceiptServer.Repositories;
using System.Net;

namespace ReceiptServer.Services
{
    public interface IReceiptService
    {
        Task<APIResponse<List<Receipt>>> GetAllReceiptsAsync();
        Task<APIResponse<Receipt>> GetReceiptByIdAsync(int id);
        Task<APIResponse<Receipt>> CreateReceiptAsync(Receipt receipt);
        Task<APIResponse<Receipt>> UpdateReceiptAsync(Receipt receipt);
        Task<APIResponse<Receipt>> DeleteReceiptAsync(int id);
    }

    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepositoriy _receiptRepository;
        public ReceiptService(IReceiptRepositoriy receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task<APIResponse<Receipt>> CreateReceiptAsync(Receipt receipt)
        {
            var response = new APIResponse<Receipt>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            try
            {
                               // Validate receipt data
                if (receipt == null || receipt.Articles == null || !receipt.Articles.Any())
                {
                    response.ErrorMessages.Add("Invalid receipt data.");
                    return response;
                }
                // Additional validation logic can be added here
                // If validation passes, create the receipt
                var createdReceipt = new Receipt
                {
                    ReceiptNumber = receipt.ReceiptNumber,
                    Date = receipt.Date,
                    Articles = receipt.Articles
                };
                await _receiptRepository.CreateReceiptAsync(createdReceipt);
                await _receiptRepository.SaveAsync();
                response.Result = createdReceipt;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);

            }
            return response;
        }



        public async Task<APIResponse<List<Receipt>>> GetAllReceiptsAsync()
        {
            var response = new APIResponse<List<Receipt>>();
            try
            {
               
                var receipts = await _receiptRepository.GetAllReceiptsAsync();
                response.Result = receipts.ToList();
                response.IsSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
            }
            return response;
        }

        public async Task<APIResponse<Receipt>> GetReceiptByIdAsync(int id)
        {// Meddelande bör ge svar om kvittonummer ist för id
            var response = new APIResponse<Receipt>();
            try
            {
                var receipt = await _receiptRepository.GetReceiptByIdAsync(id);
                if (receipt != null)
                {
                    response.Result = receipt;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Receipt with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
        

        public async Task<APIResponse<Receipt>> UpdateReceiptAsync(Receipt receipt)
        {
            var response = new APIResponse<Receipt>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotImplemented
            };

            try {                 
                var existingReceipt = await _receiptRepository.GetReceiptByIdAsync(receipt.Id);
                if (existingReceipt != null)
                {
                    // Update properties
                    existingReceipt.ReceiptNumber = receipt.ReceiptNumber;
                    existingReceipt.Date = receipt.Date;
                    existingReceipt.Articles = receipt.Articles;
                    await _receiptRepository.UpdateReceiptAsync(existingReceipt);
                    await _receiptRepository.SaveAsync();
                    response.Result = existingReceipt;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Receipt with ID {receipt.Id} not found.");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
            }
            return response;

        }

        public async Task<APIResponse<Receipt>> DeleteReceiptAsync(int id)
        {
            var response = new APIResponse<Receipt>();
            try
            {
                var receiptToDelete = await _receiptRepository.GetReceiptByIdAsync(id);
                if (receiptToDelete != null)
                {
                    await _receiptRepository.DeleteReceiptAsync(receiptToDelete);
                    await _receiptRepository.SaveAsync();

                    response.Result = receiptToDelete;
                    response.IsSuccess = true;
                    response.StatusCode = System.Net.HttpStatusCode.NoContent;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Receipt with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }

    }
}
