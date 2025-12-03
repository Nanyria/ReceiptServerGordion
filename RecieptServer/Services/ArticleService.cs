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

    public class ArticleService : IReceiptServerService<Article>
    {
        private readonly IReceiptServiceRepositoriy<Article> _articleRepository;
        public ArticleService(IReceiptServiceRepositoriy<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<APIResponse<Article>> CreateAsync(Article article)
        {
            var response = new APIResponse<Article>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            try
            {

                if (article == null || article.ReceiptArticles == null || !article.ReceiptArticles.Any())
                {
                    response.ErrorMessages.Add("Invalid receipt data.");
                    return response;
                }

                var createdArticle = new Article
                {
                    Name = article.Name,
                    Price = article.Price,
                    ReceiptArticles = article.ReceiptArticles,

                };



                await _articleRepository.CreateAsync(createdArticle);
                await _articleRepository.SaveAsync();
                response.Result = createdArticle;
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



        public async Task<APIResponse<List<Article>>> GetAllAsync()
        {
            var response = new APIResponse<List<Article>>();
            try
            {
               
                var articles = await _articleRepository.GetAllAsync();
                response.Result = articles.ToList();
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

        public async Task<APIResponse<Article>> GetByIdAsync(int id)
        {// Meddelande bör ge svar om kvittonummer ist för id
            var response = new APIResponse<Article>();
            try
            {
                var article = await _articleRepository.GetByIdAsync(id);
                if (article != null)
                {
                    response.Result = article;
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
        

        public async Task<APIResponse<Article>> UpdateAsync(Article article)
        {
            var response = new APIResponse<Article>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotImplemented
            };

            try {                 
                var existingArticle = await _articleRepository.GetByIdAsync(article.Id);
                if (existingArticle != null)
                {
                    // Update properties
                    existingArticle.Name = article.Name;
                    existingArticle.Price = article.Price;
                    existingArticle.ReceiptArticles = article.ReceiptArticles;
                    await _articleRepository.UpdateAsync(existingArticle);
                    await _articleRepository.SaveAsync();
                    response.Result = existingArticle;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"Receipt with ID {article.Id} not found.");
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

        public async Task<APIResponse<Article>> DeleteAsync(int id)
        {
            var response = new APIResponse<Article>();
            try
            {
                var articleToDelete = await _articleRepository.GetByIdAsync(id);
                if (articleToDelete != null)
                {
                    await _articleRepository.DeleteAsync(articleToDelete);
                    await _articleRepository.SaveAsync();

                    response.Result = articleToDelete;
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
