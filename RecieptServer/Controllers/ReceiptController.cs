using Microsoft.AspNetCore.Mvc;
using RecieptServer.Models;
using RecieptServer.Services;

namespace ReceiptServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptServerService<ReceiptDTO> _receiptService;
        public ReceiptController(IReceiptServerService<ReceiptDTO> receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReceipts()
        {
            var response = await _receiptService.GetAllAsync();
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSingleReceipt(int id)
        {
            var response = await _receiptService.GetByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceipt([FromBody] ReceiptDTO receiptDTO)
        {
            var response = await _receiptService.CreateAsync(receiptDTO);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{receiptId:int}")]
        public async Task<IActionResult> UpdateReceipt(int receiptId, [FromBody] ReceiptDTO receiptDTO)
        {
            receiptDTO.Id = receiptId;
            var response = await _receiptService.UpdateAsync(receiptDTO);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{receiptId:int}")]
        public async Task<IActionResult> DeleteReceipt(int receiptId)
        {
            var response = await _receiptService.DeleteAsync(receiptId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
