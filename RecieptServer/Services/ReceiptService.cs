using ReceiptServer.Models;
using ReceiptServer.Repositories;
using System.Net;
using ReceiptServer.Helpers;
using RecieptServer.Services;


namespace ReceiptServer.Services
{
    //public interface IReceiptService
    //{

    //    Task<APIResponse<Receipt>> GetReceiptByIdAsync(int id);
    //    Task<APIResponse<Receipt>> CreateReceiptAsync(Receipt receipt);
    //    Task<APIResponse<List<Receipt>>> GetAllReceiptsAsync();
    //    Task<APIResponse<Receipt>> UpdateReceiptAsync(Receipt receipt);
    //    Task<APIResponse<Receipt>> DeleteReceiptAsync(int id);
    //}

    public class ReceiptService : IReceiptServerService<Receipt>
    {
        private readonly IReceiptServiceRepositoriy<Receipt> _receiptRepository;
        public ReceiptService(IReceiptServiceRepositoriy<Receipt> receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task<APIResponse<Receipt>> CreateAsync(Receipt receipt)
        {
            var response = new APIResponse<Receipt>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            try
            {

                if (receipt == null || receipt.ReceiptArticles == null || !receipt.ReceiptArticles.Any())
                {
                    response.ErrorMessages.Add("Invalid receipt data.");
                    return response;
                }
                foreach (var article in receipt.ReceiptArticles)
                {
                    RecepitCalculator.CalculateTotalArticleAmount(article);
                }

                var createdReceipt = new Receipt
                {
                    Date = receipt.Date,
                    ReceiptArticles = receipt.ReceiptArticles,
                    TotalAmount = RecepitCalculator.CalculateTotalReceiptAmount(receipt.ReceiptArticles)
                };



                await _receiptRepository.CreateAsync(createdReceipt);
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



        public async Task<APIResponse<List<Receipt>>> GetAllAsync()
        {
            var response = new APIResponse<List<Receipt>>();
            try
            {
               
                var receipts = await _receiptRepository.GetAllAsync();
                response.Result = receipts.ToList();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add(ex.Message);
            }
            return response;
        }

        public async Task<APIResponse<Receipt>> GetByIdAsync(int id)
        {// Meddelande bör ge svar om kvittonummer ist för id
            var response = new APIResponse<Receipt>();
            try
            {
                var receipt = await _receiptRepository.GetByIdAsync(id);
                if (receipt != null)
                {
                    response.Result = receipt;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
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
        

        public async Task<APIResponse<Receipt>> UpdateAsync(Receipt receipt)
        {
            var response = new APIResponse<Receipt>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotImplemented
            };

            try {                 
                var existingReceipt = await _receiptRepository.GetByIdAsync(receipt.Id);
                if (existingReceipt != null)
                {
                    // Update properties
                    existingReceipt.Date = receipt.Date;
                    existingReceipt.ReceiptArticles = receipt.ReceiptArticles;
                    existingReceipt.TotalAmount = RecepitCalculator.CalculateTotalReceiptAmount(receipt.ReceiptArticles);
                    await _receiptRepository.UpdateAsync(existingReceipt);
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

        public async Task<APIResponse<Receipt>> DeleteAsync(int id)
        {
            var response = new APIResponse<Receipt>();
            try
            {
                var receiptToDelete = await _receiptRepository.GetByIdAsync(id);
                if (receiptToDelete != null)
                {
                    await _receiptRepository.DeleteAsync(receiptToDelete);
                    await _receiptRepository.SaveAsync();

                    response.Result = receiptToDelete;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.NoContent;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
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
