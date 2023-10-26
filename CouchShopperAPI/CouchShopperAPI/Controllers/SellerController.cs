using CouchShopper.Business.Interfaces;
using CouchShopper.Data.DTOs.Request.Seller;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Route("seller")]
    public class SellerController : BaseController
    {
        private readonly ISellerService _sellerService;
        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetSellerInfo([FromQuery] string userId)
        {
            return Ok(await _sellerService.GetSellerInfo(userId));
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetSellerDetails([FromQuery] string userId, int page)
        {
            return Ok(await _sellerService.GetSellerDetails(userId, page));
        }

        [HttpGet]
        [Route("sales")]
        public async Task<IActionResult> GetSalesInfo([FromQuery] string userId, int periodId)
        {
            return Ok(await _sellerService.GetSalesInfo(userId, periodId));
        }

        [HttpGet]
        [Route("payouts")]
        public async Task<IActionResult> GetPayouts([FromQuery] string userId, int page)
        {
            return Ok(await _sellerService.GetPayouts(userId, page));
        }

        [HttpGet]
        [Route("balance")]
        public async Task<IActionResult> GetBalance([FromQuery] string userId)
        {
            return Ok(await _sellerService.GetBalance(userId));
        }

        [HttpPost]
        [Route("withdrawal")]
        public async Task<IActionResult> Withdrawal([FromBody] SellerWithdrawalRequest request)
        {
            await _sellerService.Withdrawal(request);
            return Ok();
        }
    }
}
