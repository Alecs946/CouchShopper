using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Validators;
using CouchShopper.Data.Constants;
using CouchShopper.Data.DTOs.Request.Products;
using CouchShopper.Data.DTOs.Response.Common.Category;
using CouchShopper.Data.DTOs.Response.Products;
using CouchShopper.Data.DTOs.Response.Users;
using CouchShopper.Data.Models;
using Flurl.Util;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Services
{
    public class ProductService : BaseService<Products>, IProductService
    {
        private readonly IMapper _mapper;
        public ProductService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
        }

        public async Task<ProductAddedResponse> CreatePoduct(ProductAddRequest request)
        {
            request.Validate();
            var productToInsert = _mapper.Map<Products>(request);
            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                 $"/{Views.productByUserWithName}?key=[\"{request.UserId}\",\"{request.Name}\"]");
            if (dbResult != null)
            {
                var content = await dbResult.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(content);
                var rows = jsonObject["rows"];

                if (rows != null && rows.Any())
                {
                    throw new InvalidRequestException($"Product with this name already exists");
                }
            }
            var productPhotos = new List<ProductPhotos>();
            if (request.Photos.Any())
            {
                productPhotos.AddRange(_mapper.Map<List<ProductPhotos>>(request.Photos));
                productToInsert.DefaultPhotoId = productPhotos.First(x => x.IsDefault).Id;
            }

            var response = await InsertAsync(productToInsert);
            if (!response)
            {
                throw new Exception($"Product could not be created");
            }
            if (productPhotos.Any())
            {
                foreach (var x in productPhotos)
                {
                    var insertedProduct = await GetByIdAsync(productToInsert.Id);
                    await InsertAttachment(productToInsert.Id, x.Id, x.Content, insertedProduct.Rev);
                }
            }

            return _mapper.Map<ProductAddedResponse>(productToInsert);
        }

        public async Task DeleteProduct(ProductDeleteRequest request)
        {
            request.Validate();
            var productToDelte = await GetByIdAsync(request.ProductId);
            if (productToDelte == null)
            {
                throw new InvalidRequestException($"Product does not exist.");
            }
            productToDelte.Deleted = true;
            if (!await UpdateAsync(productToDelte))
            {
                throw new InvalidRequestException($"Product could not be deleted");
            }
        }

        public async Task<ProductResponse> GetProduct(string id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                throw new InvalidRequestException($"Product does not exist.");
            }
            var response = _mapper.Map<ProductResponse>(product);
            if (product._attachments != null)
            {
                foreach (var item in product._attachments)
                {
                    var attachment = await GetAttachemntContent(id, item.Key);
                    response.Photos.Add(new ProductPhotoResponse()
                    {
                        Id = item.Key,
                        Content = Convert.ToBase64String(attachment) ?? string.Empty,
                        IsDefault = product.DefaultPhotoId == item.Key
                    });
                }
            }

            return response;
        }

        public async Task<int> GetProductCount(string userId)
        {
            var count = 0;
            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                 $"/{Views.productsPerUser}?key=\"{userId}\"");
            if (dbResult != null)
            {
                var content = await dbResult.Content.ReadAsStringAsync();
                var parsed = JObject.Parse(content)["rows"];
                count = parsed.Any() ? parsed[0]["value"].Value<int>() : 0;
            }

            return count;
        }

        public async Task<ProductByUserListResponse> GetProductsPerUser(int page, string userId)
        {
            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                 $"/{Views.productsByUser}?key=\"{userId}\"&include_docs=true");

            if (dbResult != null)
            {
                var products = (JsonConvert.DeserializeObject<BaseViewEntity<Products>>(await dbResult.Content.ReadAsStringAsync())).Rows.Select(x => x.Doc).ToList();
                var response = new ProductByUserListResponse
                {
                    TotalEntities = products.Count,
                    Offset = (page - 1) * Numeric.ItemsPerPage,
                    Products = _mapper.Map<List<ProductsByUserResponse>>(products.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
                };
                var keys = string.Join(",", response.Products.Select(r => $"\"{r.Id}\""));
                dbResult = await client.GetAsync($"{typeof(Orders).Name.ToLower()}" +
                                                 $"/{Views.productEarnings}?keys =[{keys}]");
                if (dbResult != null)
                {
                    var earnings = (JsonConvert.DeserializeObject<ProductEarningsResponse>(await dbResult.Content.ReadAsStringAsync())).Rows.ToList();
                    foreach (var item in response.Products)
                    {
                        var earning = earnings.Where(x => x.Key == item.Id).Select(x => x.SalesEarnings).ToList();
                        item.NumberOfSales = earning.Select(x => x.Sales).Sum();
                        item.Earnings = earning.Select(x => x.Earnings).Sum();
                    }
                }
                foreach (var item in response.Products)
                {
                    var photo = await GetAttachemntContent(item.Id, item.PhotoId);
                    item.Photo.Content = photo != null ? Convert.ToBase64String(photo) : null;
                }
                return response;

            }

            return new ProductByUserListResponse();
        }

        public async Task UpdateProduct(ProductUpdateRequest request)
        {
            request.Validate();
            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                 $"/{Views.productByUserWithName}?key=[\"{request.UserId}\",\"{request.Name}\"]");
            if (dbResult != null)
            {
                var content = await dbResult.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(content);
                var rows = jsonObject["rows"];

                if (rows != null && rows.Any())
                {
                    foreach (var row in rows)
                    {
                        var id = row["id"]?.ToString();
                        if (id != request.Id)
                        {
                            throw new InvalidRequestException($"Product with this name already exists");
                        }
                    }
                }
            }
            var productToUpdate = await GetByIdAsync(request.Id);
            productToUpdate.Brand = request.Brand;
            productToUpdate.Model = request.Model;
            productToUpdate.Name = request.Name;
            productToUpdate.Price = request.Price;
            productToUpdate.SalePrice = request.SalePrice;
            productToUpdate.Quantity = request.Quantity;
            productToUpdate.ProductDescription = request.ProductDescription;
            productToUpdate.Category = request.Category;
            productToUpdate.FeatureProduct = request.FeatureProduct;
            productToUpdate._attachments = null;
            productToUpdate.Options = _mapper.Map<List<ProductOptions>>(request.Options);
            var productPhotos = new List<ProductPhotos>();
            if (request.Photos.Any())
            {
                productPhotos.AddRange(_mapper.Map<List<ProductPhotos>>(request.Photos));
                productToUpdate.DefaultPhotoId = productPhotos.First(x => x.IsDefault).Id;
            }

            var response = await UpdateAsync(productToUpdate);
            if (!response)
            {
                throw new Exception($"Product could not be created");
            }
            if (productPhotos.Any())
            {
                foreach (var x in productPhotos)
                {
                    var insertedProduct = await GetByIdAsync(productToUpdate.Id);
                    await InsertAttachment(productToUpdate.Id, x.Id, x.Content, insertedProduct.Rev);
                }
            }

        }

        public async Task<ProductFeaturedProductsResponseList> GetFeaturedProducts(int page)
        {
            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                $"/{Views.featuredProducts}?include_docs=true");

            if (dbResult != null)
            {
                var products = (JsonConvert.DeserializeObject<BaseViewEntity<Products>>(await dbResult.Content.ReadAsStringAsync())).Rows.Select(x => x.Doc).ToList();
                var response = new ProductFeaturedProductsResponseList
                {
                    TotalEntities = products.Count,
                    Offset = (page - 1) * Numeric.ItemsPerPage,
                    Products = _mapper.Map<List<ProductFeaturedProductsResponse>>(products.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
                };
                foreach (var item in response.Products)
                {
                    var photo = await GetAttachemntContent(item.Id, item.PhotoId);
                    item.Photo.Content = photo != null ? Convert.ToBase64String(photo) : null;
                }
                return response;
            }
            return new ProductFeaturedProductsResponseList
            {
                Offset = 0,
                Products = new List<ProductFeaturedProductsResponse>(),
                TotalEntities = 0
            };
        }

        public async Task<List<ProductRecentAddResponse>> GetRecentProducts(int count)
        {
            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                 $"/{Views.activeProductsWithDate}?sort=dsc&include_docs=true");

            if (dbResult != null)
            {
                var content = await dbResult.Content.ReadAsStringAsync();
                var products = (JsonConvert.DeserializeObject<BaseViewEntity<Products>>(await dbResult.Content.ReadAsStringAsync())).Rows.Select(x => x.Doc).ToList();
                var response = _mapper.Map<List<ProductRecentAddResponse>>(products.Take(count));
                var keys = string.Join(",", response.Select(r => $"\"{r.Id}\""));
                dbResult = await client.GetAsync($"{typeof(Orders).Name.ToLower()}" +
                                                 $"/{Views.productEarnings}?keys =[{keys}]");
                var earnings = dbResult != null ? (JsonConvert.DeserializeObject<ProductEarningsResponse>(await dbResult.Content.ReadAsStringAsync())).Rows.ToList() :
                new List<ProductEarningsList>();

                foreach (var item in response)
                {
                    item.NumberOfSales = earnings.Where(x => x.Key == item.Id).Select(x => x.SalesEarnings.Sales).Sum();
                    var photo = await GetAttachemntContent(item.Id, item.PhotoId);
                    item.Photo.Content = photo != null ? Convert.ToBase64String(photo) : null;
                }
                return response;
            }
            return new List<ProductRecentAddResponse>();
        }

        public async Task<string> GetPhotoContent(string productId, string photoId)
        {
            var photo = await GetAttachemntContent(productId, photoId);
            return photo != null ? Convert.ToBase64String(photo) : null;
        }

        public async Task<ProductReviewsListResponse> GetProductReviews(int page, string productId)
        {
            var product = await GetByIdAsync(productId);
            if (product != null && (product?.Reviews?.Any() ?? false))
            {
                return new ProductReviewsListResponse()
                {
                    TotalEntities = product.Reviews.Count,
                    Offset = (page - 1) * Numeric.ItemsPerPage,
                    Reviews = _mapper.Map<List<ProductReviewResponse>>(product.Reviews.OrderByDescending(x => x.AddedOn).Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
                };
            }

            return new ProductReviewsListResponse()
            {
                TotalEntities = 0,
                Offset = 0,
                Reviews = new List<ProductReviewResponse>()
            };
        }

        public async Task<ProductCartResponseList> GetCartItems(List<ProductCartItemsRequest> request)
        {
            var requestkeys = request.Select(x => x.ProductId).Distinct();
            var keys = string.Join(",", requestkeys.Select(r => $"\"{r}\""));

            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                 $"/{Views.activeProduct}?keys=[{keys}]&include_docs=true");

            if (dbResult != null)
            {
                var products = (JsonConvert.DeserializeObject<BaseViewEntity<Products>>(await dbResult.Content.ReadAsStringAsync())).Rows.Select(x => x.Doc).Distinct().ToList();
                var subtotal = 0.0;
                var cartItems = new List<ProductCartResponse>();
                foreach (var item in request)
                {
                    var product = _mapper.Map<ProductCartResponse>(products.Where(x => x.Id == item.ProductId).First());
                    product.SelectedOptions = _mapper.Map<List<ProductCartOptionResponse>>(item.SelectedOption);
                    product.Quantity = item.Quantity;
                    subtotal += product.Price * product.Quantity;
                    var photo = await GetAttachemntContent(product.ProductId, product.PhotoId);
                    product.ImageBase64 = photo != null ? Convert.ToBase64String(photo) : null;
                    cartItems.Add(product);
                }

                var resposne = new ProductCartResponseList
                {
                    Subtotal = subtotal,
                    CartItems = cartItems
                };
                return resposne;
            }

            return null;
        }

        public async Task RateProduct(ProductRatingRequest request)
        {
            request.Validate();
            var productToUpdate = await GetByIdAsync(request.ProductId);
            if (productToUpdate == null)
            {
                throw new InvalidRequestException($"Product does not exist.");
            }
            var review = new ProductReview()
            {
                UserId = request.UserId,
                Comment = request.Comment,
                Rating = request.Rating,
                AddedOn = DateTime.UtcNow
            };
            productToUpdate.Reviews ??= new List<ProductReview>();
            productToUpdate.Reviews.Add(review);
            await UpdateAsync(productToUpdate);
        }

        public async Task<List<ProductRangeResponse>> GetProductRange(List<string> productIds)
        {
            var keys = string.Join(",", productIds.Select(r => $"\"{r}\""));

            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                 $"/{Views.activeProduct}?keys=[{keys}]&include_docs=true");
            if (dbResult != null)
            {
                var products = _mapper.Map<List<ProductRangeResponse>>((JsonConvert.DeserializeObject<BaseViewEntity<Products>>(await dbResult.Content.ReadAsStringAsync())).Rows.Select(x => x.Doc).Distinct().ToList());
                foreach (var item in products)
                {
                    var photo = await GetAttachemntContent(item.ProductId, item.PhotoId);
                    item.ImageBase64 = photo != null ? Convert.ToBase64String(photo) : null;
                }
                return products;
            }
            return null;
        }

        public async Task<ProductSearchResponseList> ProductSearch(string searchPhrase, string category, int page)
        {
            var mangoQuery = string.Empty;
            if (!string.IsNullOrEmpty(searchPhrase) && string.IsNullOrEmpty(category))
            {
                mangoQuery = $"{{\"selector\":" +
                   $" {{\"$or\": " +
                    $"[" +
                    $"{{\"Brand\": {{\"$regex\": \"(?i){searchPhrase}\"}}}}," +
                    $"{{\"Model\": {{\"$regex\": \"(?i){searchPhrase}\"}}}}," +
                    $"{{\"Name\": {{\"$regex\": \"(?i){searchPhrase}\"}}}}," +
                    $"{{\"ProductDescription\": {{\"$regex\": \"(?i){searchPhrase}\"}}}}]}}," +
                    $"\"use_index\": \"search-index\"}}";
            }
            if (string.IsNullOrEmpty(searchPhrase) && !string.IsNullOrEmpty(category))
            {
                mangoQuery = $"{{\"selector\": " +
                    $"{{\"Category\": " +
                    $"{{\"$regex\": \"(?i){category}\"}}" +
                    $"}}," +
                    $"\"use_index\": \"search-index\"" +
                    $"}}";
            }
            if (!string.IsNullOrEmpty(searchPhrase) && !string.IsNullOrEmpty(category))
            {
                mangoQuery = $"{{\"selector\": {{\"$and\": " +
                    $"[{{\"$or\": " +
                    $"[" +
                    $"{{\"Name\": {{\"$regex\": \"(?i){searchPhrase}\"}}}}," +
                    $"{{\"Brand\": {{\"$regex\": \"(?i){searchPhrase}\"}}}}," +
                    $"{{\"Model\": {{\"$regex\": \"(?i){searchPhrase}\"}}}}," +
                    $"{{\"ProductDescription\": {{\"$regex\": \"(?i){searchPhrase}\"}}}}" +
                    $"]}}," +
                    $"{{\"$and\": " +
                    $"[{{\"Category\": {{\"$ne\": null}}}}," +
                    $"{{\"Category\": {{\"$regex\": \"(?i){category}\"}}}}]" +
                    $"}}]}}," +
                    $"\"use_index\": \"search-index\"\r\n}}";
            }



            var content = new StringContent(mangoQuery, Encoding.UTF8, "application/json");
            var requestUrl = $"{typeof(Products).Name.ToLower()}/_find";

            var dbResult = await client.PostAsync(requestUrl, content);

            if (dbResult.IsSuccessStatusCode)
            {
                var products = JsonConvert.DeserializeObject<MangoQuery<Products>>(await dbResult.Content.ReadAsStringAsync()).Documents.Where(x=>!x.Deleted).ToList();
                var response = new ProductSearchResponseList
                {
                    TotalEntities = products.Count,
                    Offset = (page - 1) * Numeric.ItemsPerPage,
                    Products = _mapper.Map<List<ProductSearchResponse>>(products.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
                };
                foreach (var item in response.Products)
                {
                    var attachment = await GetAttachemntContent(item.Id, item.PhotoId);
                    item.Photo = new ProductPhotoResponse()
                    {
                        Id = item.PhotoId,
                        Content = attachment != null ? Convert.ToBase64String(attachment) : null,
                        IsDefault = true
                    };
                }
                return response;
            }
            return null;
        }

    }
}
