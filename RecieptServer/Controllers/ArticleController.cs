using Microsoft.AspNetCore.Mvc;
using ReceiptServer.Models;
using ReceiptServer.Repositories;
using ReceiptServer.Services;

namespace ReceiptServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly ArticleSer _articleService;
        public ArticleController(ArticleSer articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var response = await _articleService.GetAllAsync();
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("getSingle/{getSingle}")]
        public IActionResult GetSingleArticle(int getSingle)
        {
            var response = _articleService.GetByIdAsync(getSingle);
            return StatusCode((int)response.Result.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReceipt([FromBody] Article article)
        {
            var response = await _articleService.CreateAsync(article);
            return StatusCode((int)response.StatusCode, response);

        }
        [HttpPut("{receiptId:int}")]
        public async Task<IActionResult> UpdateArticle(int articleId, [FromBody] Article article)
        {
            article.Id = articleId;
            var response = await _articleService.UpdateAsync(article);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("{receiptId:int}")]
        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            var response = await _articleService.DeleteAsync(articleId);
            return StatusCode((int)response.StatusCode, response);
        }


    }
}
