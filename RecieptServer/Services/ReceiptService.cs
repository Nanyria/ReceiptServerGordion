using ReceiptServer.Models;
using ReceiptServer.Repositories;
using System.Net;
using ReceiptServer.Helpers;
using RecieptServer.Services;
using RecieptServer.Models;
using AutoMapper;


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

    public class ReceiptService : IReceiptServerService<ReceiptDTO>
    {
        private readonly IReceiptServiceRepositoriy<Receipt> _receiptRepository;
        private readonly IMapper _mapper;
        public ReceiptService(IReceiptServiceRepositoriy<Receipt> receiptRepository, IMapper mapper)
        {
            _receiptRepository = receiptRepository;
            _mapper = mapper;
        }

        public async Task<APIResponse<ReceiptDTO>> CreateAsync(ReceiptDTO receiptDTO)
        {
            var response = new APIResponse<ReceiptDTO>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            try
            {

                if (receiptDTO == null || receiptDTO.ReceiptArticles == null || !receiptDTO.ReceiptArticles.Any())
                {
                    response.ErrorMessages.Add("Invalid receipt data.");
                    return response;
                }
                foreach (var article in receiptDTO.ReceiptArticles)
                {
                    ReceiptCalculator.CalculateTotalArticleAmount(article);
                }

                var receiptArticles = _mapper.Map<List<ReceiptArticle>>(receiptDTO.ReceiptArticles);
                var receiptTotal = ReceiptCalculator.CalculateTotalReceiptAmount(receiptDTO.ReceiptArticles);
                var createdReceipt = new Receipt
                {
                    Date = receiptDTO.Date,
                    ReceiptArticles = receiptArticles,
                    TotalAmount = receiptTotal
                };



                await _receiptRepository.CreateAsync(createdReceipt);
                await _receiptRepository.SaveAsync();
                response.Result = _mapper.Map<ReceiptDTO>(createdReceipt);
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



        public async Task<APIResponse<List<ReceiptDTO>>> GetAllAsync()
        {
            var response = new APIResponse<List<ReceiptDTO>>();
            try
            {
               
                var receipts = await _receiptRepository.GetAllAsync();
                var dtoList = _mapper.Map<List<ReceiptDTO>>(receipts);

                response.Result = dtoList;
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

        public async Task<APIResponse<ReceiptDTO>> GetByIdAsync(int id)
        {
            var response = new APIResponse<ReceiptDTO>();
            try
            {
                var receipt = await _receiptRepository.GetByIdAsync(id);
                var receiptDTO = _mapper.Map<ReceiptDTO>(receipt);
                if (receipt != null)
                {
                    response.Result = receiptDTO;
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
        

        public async Task<APIResponse<ReceiptDTO>> UpdateAsync(ReceiptDTO receiptDTO)
        {
            var response = new APIResponse<ReceiptDTO>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotImplemented
            };

            try {                 
                var existingReceipt = await _receiptRepository.GetByIdAsync(receiptDTO.Id);
                if (existingReceipt != null && existingReceipt.Id == receiptDTO.Id)
                {
                    var receiptArticles = _mapper.Map<List<ReceiptArticle>>(receiptDTO.ReceiptArticles);
                    // Update properties
                    existingReceipt.Date = receiptDTO.Date;
                    existingReceipt.ReceiptArticles = receiptArticles;
                    existingReceipt.TotalAmount = ReceiptCalculator.CalculateTotalReceiptAmount(receiptDTO.ReceiptArticles);
                    await _receiptRepository.UpdateAsync(existingReceipt);
                    await _receiptRepository.SaveAsync();
                    response.Result = _mapper.Map<ReceiptDTO>(existingReceipt);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Receipt with ID {receiptDTO.Id} not found.");
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

        public async Task<APIResponse<ReceiptDTO>> DeleteAsync(int id)
        {
            var response = new APIResponse<ReceiptDTO>();
            try
            {
                var receiptToDelete = await _receiptRepository.GetByIdAsync(id);
                if (receiptToDelete != null)
                {
                    await _receiptRepository.DeleteAsync(receiptToDelete);
                    await _receiptRepository.SaveAsync();

                    response.Result = _mapper.Map<ReceiptDTO>(receiptToDelete); 
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
