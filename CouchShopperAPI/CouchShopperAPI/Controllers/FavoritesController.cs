using CouchShopper.Business.Interfaces;
using CouchShopper.Data.DTOs.Request.Favorites;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Route("favorites")]
    public class FavoritesController : BaseController
    {
        private readonly IFavoritesService _favoritesService;
        public FavoritesController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpPut]
        [Route("add")]
        public async Task<IActionResult> UpdateProfileDetails(FavoritesAddRequest request)
        {
            return Ok(await _favoritesService.AddToFavorites(request));
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetUserFavorites([FromQuery] string userId)
        {
            return Ok(await _favoritesService.GetUserFavorites(userId));
        }

        [HttpGet]
        [Route("cout")]
        public async Task<IActionResult> GetFavoritesCount([FromQuery] string userId)
        {
            return Ok(await _favoritesService.GetFavoritesCount(userId));
        }

    }
}
