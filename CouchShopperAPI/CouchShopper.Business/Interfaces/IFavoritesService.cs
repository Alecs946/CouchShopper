using CouchShopper.Data.DTOs.Request.Favorites;
using CouchShopper.Data.DTOs.Response.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface IFavoritesService
    {
        Task<string> AddToFavorites(FavoritesAddRequest request);

        Task RemoveFavorites(FavoritesDeleteRequest request);

        Task<List<UserFavoritesResponse>> GetUserFavorites(string userId);

        Task<int> GetFavoritesCount(string userId);
    }
}
