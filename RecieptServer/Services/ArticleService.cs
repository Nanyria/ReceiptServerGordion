using ReceiptServer.Models;
using ReceiptServer.Repositories;
using System.Net;
using ReceiptServer.Helpers;
using RecieptServer.Services;
using RecieptServer.Models;
using AutoMapper;
using System.Linq;


namespace ReceiptServer.Services
{
    public class ArticleService : IReceiptServerService<ArticleDTO>
    {
        private readonly IReceiptServiceRepositoriy<Article> _receiptRepository;
        private readonly IMapper _mapper;
        public ArticleService(IReceiptServiceRepositoriy<Article> receiptRepository, IMapper mapper)
        {
            _receiptRepository = receiptRepository;
            _mapper = mapper;
        }

        public async Task<APIResponse<ArticleDTO>> CreateAsync(ArticleDTO articleDTO)
        {
            var response = new APIResponse<ArticleDTO>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            try
            {

                if (articleDTO == null )
                {
                    response.ErrorMessages.Add("Invalid receipt data.");
                    return response;
                }

                var createdArticle = new Article
                {
                    Name = articleDTO.Name,
                    Price = articleDTO.Price,
                    ReceiptArticles = new List<ReceiptArticle>()
                };

                await _receiptRepository.CreateAsync(createdArticle);
                await _receiptRepository.SaveAsync();
                response.Result = _mapper.Map<ArticleDTO>(createdArticle);
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

        public async Task<APIResponse<List<ArticleDTO>>> GetAllAsync()
        {
            var response = new APIResponse<List<ArticleDTO>>();
            try
            {
                var articles = await _receiptRepository.GetAllAsync();
                var dtoList = _mapper.Map<List<ArticleDTO>>(articles);

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

        public async Task<APIResponse<ArticleDTO>> GetByIdAsync(int id)
        {
            var response = new APIResponse<ArticleDTO>();
            try
            {
                var article = await _receiptRepository.GetByIdAsync(id);
                var articleDTO = _mapper.Map<ArticleDTO >(article);
                if (article != null)
                {
                    response.Result = articleDTO;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Article with ID {id} not found.");
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

        public async Task<APIResponse<ArticleDTO>> UpdateAsync(ArticleDTO articleDTO)
        {
            var response = new APIResponse<ArticleDTO>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotImplemented
            };

            try {                 
                var existingArticle = await _receiptRepository.GetByIdAsync(articleDTO.Id);
                if (existingArticle != null && existingArticle.Id == articleDTO.Id)
                {
                    var recieptArticles = _mapper.Map<List<ReceiptArticle>>(articleDTO.ReceiptArticles);
                    existingArticle.Name = articleDTO.Name;
                    existingArticle.Price = articleDTO.Price;
                    existingArticle.ReceiptArticles = recieptArticles;
                    await _receiptRepository.UpdateAsync(existingArticle);
                    await _receiptRepository.SaveAsync();
                    response.Result = _mapper.Map<ArticleDTO>(existingArticle);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Receipt with ID {articleDTO.Id} not found.");
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

        public async Task<APIResponse<ArticleDTO>> DeleteAsync(int id)
        {
            var response = new APIResponse<ArticleDTO>();
            try
            {
                var articleToDelete = await _receiptRepository.GetByIdAsync(id);
                if (articleToDelete != null)
                {
                    await _receiptRepository.DeleteAsync(articleToDelete);
                    await _receiptRepository.SaveAsync();

                    response.Result = _mapper.Map<ArticleDTO>(articleToDelete);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.NoContent;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Article with ID {id} not found.");
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
