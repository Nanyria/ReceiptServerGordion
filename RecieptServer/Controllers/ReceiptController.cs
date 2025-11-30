using Microsoft.AspNetCore.Mvc;
using ReceiptServer.Repositories;
using ReceiptServer.Services;

namespace ReceiptServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {

        private readonly IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReceipts()
        {
            var response = await _receiptService.GetAllReceiptsAsync();
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("getSingle/{getSingle}")]
        public IActionResult GetSingleReceipt(int getSingle)
        {
            var response = _receiptService.GetReceiptByIdAsync(getSingle);
            return StatusCode((int)response.Result.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReceipt([FromBody] Models.Receipt receipt)
        {
            var response = await _receiptService.CreateReceiptAsync(receipt);
            return StatusCode((int)response.StatusCode, response);

        }
        [HttpPut("{receiptId:int}")]
        public async Task<IActionResult> UpdateReceipt(int receiptId, [FromBody] Models.Receipt receipt)
        {
            receipt.Id = receiptId;
            var response = await _receiptService.UpdateReceiptAsync(receipt);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("{receiptId:int}")]
        public async Task<IActionResult> DeleteReceipt(int receiptId)
        {
            var response = await _receiptService.DeleteReceiptAsync(receiptId);
            return StatusCode((int)response.StatusCode, response);
        }


    }
}
