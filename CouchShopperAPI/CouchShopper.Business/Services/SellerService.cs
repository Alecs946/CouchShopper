using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Helpers;
using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Validators;
using CouchShopper.Data.Constants;
using CouchShopper.Data.DTOs.Request.Seller;
using CouchShopper.Data.DTOs.Response.Account;
using CouchShopper.Data.DTOs.Response.Products;
using CouchShopper.Data.DTOs.Response.Seller;
using CouchShopper.Data.enums;
using CouchShopper.Data.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Services
{
    public class SellerService : BaseService<Users>, ISellerService
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public SellerService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper, IProductService productService)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<SellerByRatingResponse> GetTopSeller()
        {
            var dbResult = await client.GetAsync($"{typeof(Users).Name.ToLower()}" +
                                                 $"/{Views.userRating}");

            if (dbResult != null)
            {
                var content = await dbResult.Content.ReadAsStringAsync();
                var parsed = ((JArray)JObject.Parse(content)["rows"]);
                if (parsed.Any())
                {
                    var topRated = parsed
                        .Select(row => new
                        {
                            Key = (string)row["key"],
                            Value = (double)row["value"]
                        })
                        .OrderByDescending(kv => kv.Value)
                        .FirstOrDefault().Key;
                    var topSeller = await GetByIdAsync(topRated);
                    var products = _mapper.Map<List<ProductShortOverviewResponse>>(JsonConvert.DeserializeObject<BaseViewEntity<Products>>
                                                                                (await (await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                                                $"/{Views.productsByUser}?key=\"{topSeller.UserName}\"&include_docs=true"))
                                                                                .Content.ReadAsStringAsync()).Rows.Select(x => x.Doc).OrderByDescending(x => x.CreatedOn).Take(6).ToList());
                    var keys = string.Join(",", products.Select(r => $"\"{r.Id}\""));
                    dbResult = await client.GetAsync($"{typeof(Orders).Name.ToLower()}" +
                                                     $"/{Views.productEarnings}?keys =[{keys}]");
                    var earnings = dbResult != null ? (JsonConvert.DeserializeObject<ProductEarningsResponse>(await dbResult.Content.ReadAsStringAsync())).Rows.ToList() : new List<ProductEarningsList>();
                    foreach (var item in products)
                    {
                        item.ImageBase64 = await _productService.GetPhotoContent(item.Id, item.PhotoId);
                        var earning = earnings.Where(x => x.Key == item.Id).Select(x => x.SalesEarnings).ToList();
                        item.NumberOfSales = earning.Select(x => x.Sales).Sum();
                    }
                    var profilePicture = await GetAttachemntContent(topSeller.Id, "profilePicture");
                    var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;

                    return new SellerByRatingResponse
                    {
                        Id = topSeller.Id,
                        SellerName = topSeller.Id,
                        ImageBase64 = profilePicture != null ? Convert.ToBase64String(profilePicture) : null,
                        MemberSince = $"{dateTimeFormat.GetMonthName(topSeller.CreatedOn.Month)} {topSeller.CreatedOn.Year}",
                        Products = products
                    };
                }
                return new SellerByRatingResponse();


            }
            return null;
        }

        public async Task RateUser(SellerRatingRequest request)
        {
            request.Validate();
            var userToUpdate = await GetByIdAsync(request.SellerId) ?? throw new InvalidRequestException($"User does not exist.");
            var userReview = new UserReview()
            {
                UserId = request.UserId,
                Rating = request.Rating,
                AddedOn = DateTime.UtcNow
            };
            userToUpdate.Reviews ??= new List<UserReview>();
            userToUpdate.Reviews.Add(userReview);
            await UpdateAsync(userToUpdate);
        }

        public async Task<SellerInfoResponse> GetSellerInfo(string userId)
        {
            var seller = await GetByIdAsync(userId);
            var profilePicture = await GetAttachemntContent(seller.Id, "profilePicture");
            return new SellerInfoResponse
            {
                SellerName = seller.Id,
                ImageBase64 = profilePicture != null ? Convert.ToBase64String(profilePicture) : null,
                MemberSince = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(seller.CreatedOn.Month)} {seller.CreatedOn.Year}",
                Rating = (seller.Reviews?.Select(x => x.Rating).Sum() / seller.Reviews?.Count) ?? 0,
                NumberOfReviews = seller.Reviews?.Count ?? 0
            };
        }

        public async Task<SellerDetailsResponse> GetSellerDetails(string sellerId, int page)
        {
            var sellerInfo = await GetSellerInfo(sellerId);
            var products = await _productService.GetProductsPerUser(page, sellerId);
            var dbResult = await client.GetAsync($"{typeof(Orders).Name.ToLower()}" +
                                                 $"/{Views.orderBySeller}?group=true&&key=\"{sellerId}\"");
            var totalSales = 0;
            if (dbResult != null && dbResult.IsSuccessStatusCode)
            {
                var content = await dbResult.Content.ReadAsStringAsync();
                totalSales = (int)((JArray)JObject.Parse(content)["rows"])
                    .Select(row => new
                    {
                        Key = (string)row["key"],
                        Value = (double)row["value"]
                    }).FirstOrDefault().Value;
            }

            return new SellerDetailsResponse()
            {
                SellerName = sellerInfo.SellerName,
                MemberSince = sellerInfo.MemberSince,
                ImageBase64 = sellerInfo.ImageBase64,
                Products = products,
                NumberOfReviews = sellerInfo.NumberOfReviews,
                Rating = sellerInfo.Rating,
                TotalSales = totalSales
            };
        }

        public async Task<SellerSalesInfoResponse> GetSalesInfo(string sellerId, int periodId)
        {
            var startDate = ((SalesPeriod)periodId).GetPeriod();
            var endDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).AddDays(1);
            var dbResult = await client.GetAsync($"{typeof(Users).Name.ToLower()}" +
                                                 $"/{Views.userEarnings}?startkey=[\"{sellerId}\",\"{startDate.ToString("yyyy-MM-dd")}\"]" +
                                                 $"&endkey=[\"{sellerId}\",\"{endDate.ToString("yyyy-MM-dd")}\"]");
            var salesDetails = new List<SellerSalesDetails>();
            if (dbResult != null)
            {
                var sales = (JsonConvert.DeserializeObject<SellerSalesResponseList>(await dbResult.Content.ReadAsStringAsync())).Rows.Select(x => x.Revenue).ToList();
                var date = startDate.AddDays(1);
                while (date < endDate)
                {
                    var onDate = sales.Where(x => x.Date.Date == date.Date).Sum(x=>x.Revenue);
                    salesDetails.Add(
                        new SellerSalesDetails()
                        {
                            Date = date.ToString("dd MMM"),
                            Revenue = onDate
                        }) ;
                    date = date.AddDays(1);
                }

            }
            dbResult = await client.GetAsync($"{typeof(Orders).Name.ToLower()}" +
                                                 $"/{Views.ordersInPeriod}?startkey=[\"{sellerId}\",\"{startDate.ToString("yyyy-MM-dd")}\"]" +
                                                 $"&endkey=[\"{sellerId}\",\"{endDate.ToString("yyyy-MM-dd")}\"]");
            var salesProducts = new List<SellerSalesProducts>();
            if (dbResult != null)
            {
                var products = (JsonConvert.DeserializeObject<SellerSalesProductList>(await dbResult.Content.ReadAsStringAsync())).Rows.GroupBy(x => x.Revenue.ProductName).ToList();
                foreach (var item in products)
                {
                    var product = item.Select(x => x.Revenue).ToList();
                    salesProducts.Add(new SellerSalesProducts
                    {
                        ProductName = product.FirstOrDefault().ProductName,
                        Quantity = product.Select(x => x.Quantity).Sum(),
                        Revenue = product.Select(x => x.Revenue * x.Quantity).Sum(),
                    });

                }
            }

            return new SellerSalesInfoResponse { SalesDetails = salesDetails, SalesProducts = salesProducts };
        }

        public async Task<double> GetBalance(string userId)
        {
            var user = await GetByIdAsync(userId);

            return Math.Round(user?.Balance ?? 0.0, 2);
        }

        public async Task<SellerPayoutsResponseList> GetPayouts(string userId, int page)
        {
            var user = await GetByIdAsync(userId);
            return new SellerPayoutsResponseList()
            {
                Balance = Math.Round(user?.Balance ?? 0.0, 2),
                PaymentMethod = _mapper.Map<SellerPaymentMethodResponse>(user?.PaymentMethods?.FirstOrDefault(x => x.IsPrimary)),
                TotalEntities = user?.Payouts?.Count ?? 0,
                Payouts = _mapper.Map<List<SellerPayoutResponse>>((user.Payouts ?? new List<UserPayout>()).Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
            };
        }

        public async Task AddRevenue(SellerRevenue request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.UserId);
            var keys = string.Join(",", request.ProductIds.Select(r => $"\"{r.Item2}\""));

            var dbResult = await client.GetAsync($"{typeof(Products).Name.ToLower()}" +
                                                 $"/{Views.activeProduct}?keys=[{keys}]&include_docs=true");
            if (dbResult != null)
            {
                var products = (JsonConvert.DeserializeObject<BaseViewEntity<Products>>(await dbResult.Content.ReadAsStringAsync()))
                    .Rows.Select(x => x.Doc).Distinct().ToList();
                var revenue = 0.0;
                foreach (var item in request.ProductIds)
                {
                    var product = products.FirstOrDefault(x => x.Id == item.Item2);
                    var percent = (1.0 - ((product.FeatureProduct ? Numeric.InterestPercentFeatured : Numeric.InterestPercent) / 100.0));
                    revenue += (double)(item.Item1 * (product?.SalePrice != null ? product.SalePrice : product.Price))
                        * (1.0 - ((product.FeatureProduct ? Numeric.InterestPercentFeatured : Numeric.InterestPercent) / 100.0));
                }
                if (user.Earnings==null)
                {
                    user.Earnings = new List<UserEarning>();
                }
                user.Earnings.Add(new UserEarning() { Earning = revenue, OnDate = DateTime.UtcNow.Date });
                user.Balance += revenue;
                await UpdateAsync(user);
            }

        }

        public async Task Withdrawal(SellerWithdrawalRequest request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.UserId);
            //transactio to users account 
            if (user.Payouts == null)
            {
                user.Payouts = new List<UserPayout>();
            }
            user.Payouts.Add(new UserPayout
            {
                Amount = user.Balance,
                OnDate = DateTime.UtcNow,
                PayoutMethod = user.PaymentMethods.FirstOrDefault(x => x.IsPrimary).CardName.ToUpper()
            });
            user.Balance = 0;
            await UpdateAsync(user);
        }
    }
}
