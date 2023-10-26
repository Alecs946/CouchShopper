using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Validators;
using CouchShopper.Data.DTOs.Request.Favorites;
using CouchShopper.Data.DTOs.Response.Users;
using CouchShopper.Data.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CouchShopper.Business.Services
{
    public class FavoritesService : BaseService<Users>, IFavoritesService
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public FavoritesService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper, IProductService productService)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<string> AddToFavorites(FavoritesAddRequest request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new InvalidRequestException($"User not found");
            }
            user.Favorites ??= new List<string>();
            if (user.Favorites.Any(x => x.Equals(request.ProductId)))
            {
                return JsonConvert.SerializeObject("Product already exists");
            }
            user.Favorites.Add(request.ProductId);
            await UpdateAsync(user);

            return JsonConvert.SerializeObject("Product added to favorites");
        }
        public async Task RemoveFavorites(FavoritesDeleteRequest request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new InvalidRequestException($"User not found");
            }
            var favoriteToRemove = user.Favorites.Where(x => x.Equals(request.ProductId)).FirstOrDefault();
            if (favoriteToRemove == null)
            {
                throw new InvalidRequestException($"Product not found");
            }
            await UpdateAsync(user);
        }

        public async Task<List<UserFavoritesResponse>> GetUserFavorites(string userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidRequestException($"User not found");
            }

            return user.Favorites != null ? _mapper.Map<List<UserFavoritesResponse>>(await _productService.GetProductRange(user.Favorites)) : null;
        }

        public async Task<int> GetFavoritesCount(string userId)
        {
            var user = await GetByIdAsync(userId);

            return user?.Favorites?.Count ?? 0;
        }
    }
}
