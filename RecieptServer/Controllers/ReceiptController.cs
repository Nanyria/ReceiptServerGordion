using Microsoft.AspNetCore.Mvc;
using ReceiptServer.Repositories;
using ReceiptServer.Services;

namespace ReceiptServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {

        private readonly ReceiptService _receiptService;
        public ReceiptController(ReceiptService receiptService)
        {
            _receiptService = receiptService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReceipts()
        {
            var response = await _receiptService.GetAllAsync();
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("getSingle/{getSingle}")]
        public IActionResult GetSingleReceipt(int getSingle)
        {
            var response = _receiptService.GetByIdAsync(getSingle);
            return StatusCode((int)response.Result.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReceipt([FromBody] Models.Receipt receipt)
        {
            var response = await _receiptService.CreateAsync(receipt);
            return StatusCode((int)response.StatusCode, response);

        }
        [HttpPut("{receiptId:int}")]
        public async Task<IActionResult> UpdateReceipt(int receiptId, [FromBody] Models.Receipt receipt)
        {
            receipt.Id = receiptId;
            var response = await _receiptService.UpdateAsync(receipt);
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
