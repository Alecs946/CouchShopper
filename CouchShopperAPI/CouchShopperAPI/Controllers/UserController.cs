using CouchShopper.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using CouchShopper.Data.Helpers;
using Microsoft.AspNetCore.Routing;
using CouchShopper.Data.DTOs.Request.Users;
using CouchShopper.Business.Services;
using CouchShopper.Data.DTOs.Request.Account;

namespace CouchShopper.API.Controllers
{
    [Route("user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserAddRequest request)
        {
            return Ok(await _userService.Create(request));
        }

        [HttpPost]
        [Route("is-active")]
        public async Task<IActionResult> IsActiveAccount([FromBody] UserActivationCheckRequest request)
        {
            return Ok(await _userService.IsActiveAccount(request));
        }

        [HttpPost]
        [Route("activate-account")]
        public async Task<IActionResult> ActiveAccount([FromBody] UserActivationRequest request)
        {
            await _userService.ActivateAccount(request);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            return Ok(await _userService.Login(request));
        }
        ////////
        //[HttpGet]
        //[Route("get-seller-info")]
        //public async Task<IActionResult> GetSellerInfo([FromQuery] string userId)
        //{
        //    return Ok(await _userService.GetSellerInfo(userId));
        //}

        //[HttpPut]
        //[Route("user-add-favorites")]
        //public async Task<IActionResult> UpdateProfileDetails(UserFavoritesRequest request)
        //{
        //    return Ok(await _userService.AddToFavorites(request));
        //}

        //[HttpGet]
        //[Route("user-get-favorites")]
        //public async Task<IActionResult> GetUserFavorites([FromQuery] string userId)
        //{
        //    return Ok(await _userService.GetUserFavorites(userId));
        //}
        ////////
        //[HttpGet]
        //[Route("user-seller-details")]
        //public async Task<IActionResult> GetSellerDetails([FromQuery] string userId,int page)
        //{
        //    return Ok(await _userService.GetSellerDetails(userId, page));
        //}
        //////////
        //[HttpGet]
        //[Route("user-sales-info")]
        //public async Task<IActionResult> GetSalesInfo([FromQuery] string userId,int periodId)
        //{
        //    return Ok(await _userService.GetSalesInfo(userId, periodId));
        //}
        /////////
        //[HttpGet]
        //[Route("user-balance")]
        //public async Task<IActionResult> GetBalance([FromQuery] string userId)
        //{
        //    return Ok(await _userService.GetBalance(userId));
        //}
        
        //[HttpGet]
        //[Route("user-favorites-cout")]
        //public async Task<IActionResult> GetFavoritesCount([FromQuery] string userId)
        //{
        //    return Ok(await _userService.GetFavoritesCount(userId));
        //}
       ////////////
        //[HttpGet]
        //[Route("user-payouts")]
        //public async Task<IActionResult> GetPayouts([FromQuery] string userId,int page)
        //{
        //    return Ok(await _userService.GetPayouts(userId, page));
        //}
    }
}
