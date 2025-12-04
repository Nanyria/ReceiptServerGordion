using Microsoft.AspNetCore.Mvc;
using ReceiptServer.Models;
using ReceiptServer.Repositories;
using ReceiptServer.Services;
using RecieptServer.Models;
using RecieptServer.Services;

namespace ReceiptServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IReceiptServerService<ArticleDTO> _articleService;
        public ArticleController(IReceiptServerService<ArticleDTO> articleService)
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
        public async Task<IActionResult> GetSingleArticle(int getSingle)
        {
            var response = await _articleService.GetByIdAsync(getSingle);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleCreateDTO createDto)
        {
            if (createDto == null)
                return BadRequest();

            // Map create DTO -> ArticleDTO (ReceiptArticles left as default empty list)
            var articleDto = new ArticleDTO
            {
                Name = createDto.Name,
                Price = createDto.Price
            };

            var response = await _articleService.CreateAsync(articleDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{articleId:int}")]
        public async Task<IActionResult> UpdateArticle(int articleId, [FromBody] ArticleDTO articleDTO)
        {
            articleDTO.Id = articleId;
            var response = await _articleService.UpdateAsync(articleDTO);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{articleId:int}")]
        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            var response = await _articleService.DeleteAsync(articleId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
