using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Services;
using CouchShopper.Data.DTOs.Request.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Authorize]
    [Route("account")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("profile-details")]
        public async Task<IActionResult> GetProfileDetails(string username)
        {
            return Ok(await _accountService.GetProfileDetails(username));
        }

        [HttpGet]
        [Route("profile-picture")]
        public async Task<IActionResult> GetProfilePicture(string username)
        {
            return Ok(await _accountService.GetProfilePicture(username));
        }

        [HttpPut]
        [Route("update-profile-details")]
        public async Task<IActionResult> UpdateProfileDetails(AccountUpdateRequest request)
        {
            await _accountService.UpdateProfileDetails(request);
            return Ok();
        }

        [HttpPut]
        [Route("update-password")]
        public async Task<IActionResult> UpdatePassword(AccountPasswordUpdateRequest request)
        {
            await _accountService.UpdatePassword(request);
            return Ok();
        }

        [HttpPost]
        [Route("upload-profile-picture")]
        public async Task<IActionResult> UploadProfilePicture([FromBody] AccountProfilePictureUploadRequest request)
        {
            await _accountService.UploadProfilePicture(request);
            return Ok();
        }

        [HttpGet]
        [Route("payment-methods")]
        public async Task<IActionResult> GetPaymentMethods(string username)
        {
            return Ok(await _accountService.GetPaymentMethods(username));
        }

        [HttpPost]
        [Route("create-card")]
        public async Task<IActionResult> CreateCard([FromBody] AccountAddPaymentCreditCardRequest request)
        {
            await _accountService.CreateCard(request);
            return Ok();
        }

        [HttpPost]
        [Route("create-paypal")]
        public async Task<IActionResult> CreatePaypal([FromBody] AccountAddPayPal request)
        {
            await _accountService.CreatePaypal(request);
            return Ok();
        }

        [HttpPut]
        [Route("update-payment-method")]
        public async Task<IActionResult> UpdatePrimaryPayment([FromBody] AccountPaymentMethodUpdateRequest request)
        {
            await _accountService.UpdatePrimaryPayment(request);
            return Ok();
        }

        [HttpDelete]
        [Route("delete-payment-method")]
        public async Task<IActionResult> DeletePaymentMethod([FromBody] AccountPaymentMethodDeleteRequest request)
        {
            await _accountService.DeletePaymentMethod(request);
            return Ok();
        }
    }
}
