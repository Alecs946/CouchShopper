using CouchShopper.Data.DTOs.Request.Products;
using CouchShopper.Data.DTOs.Response.Common.Country;
using CouchShopper.Data.DTOs.Response.Products;
using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> GetProduct(string id);

        Task<int> GetProductCount(string userId);

        Task<ProductByUserListResponse> GetProductsPerUser(int page, string userId);

        Task<ProductAddedResponse> CreatePoduct(ProductAddRequest request);

        Task UpdateProduct(ProductUpdateRequest request);

        Task DeleteProduct(ProductDeleteRequest request);

        Task<ProductFeaturedProductsResponseList> GetFeaturedProducts(int page);

        Task<List<ProductRecentAddResponse>> GetRecentProducts(int count);

        Task<string> GetPhotoContent(string productId,string photoId);

        Task<ProductReviewsListResponse> GetProductReviews( int page, string productId);

        Task<ProductCartResponseList> GetCartItems(List<ProductCartItemsRequest> request);

        Task RateProduct(ProductRatingRequest request);

        Task<List<ProductRangeResponse>> GetProductRange(List<string> productIds);

        Task<ProductSearchResponseList> ProductSearch(string searchPhrase,string category, int page);

    }
}
