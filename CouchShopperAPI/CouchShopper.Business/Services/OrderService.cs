using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Helpers;
using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Validators;
using CouchShopper.Data.Constants;
using CouchShopper.Data.DTOs.Request.Orders;
using CouchShopper.Data.DTOs.Request.Products;
using CouchShopper.Data.DTOs.Request.Seller;
using CouchShopper.Data.DTOs.Request.Users;
using CouchShopper.Data.DTOs.Response.Order;
using CouchShopper.Data.DTOs.Response.Users;
using CouchShopper.Data.enums;
using CouchShopper.Data.Extensions;
using CouchShopper.Data.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Services
{
    public class OrderService : BaseService<Orders>, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;

        public OrderService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper, IProductService productService, ISellerService sellerService)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
            _productService = productService;
            _sellerService = sellerService;
        }

        public async Task CreateOrder(OrderAddRequest request)
        {
            request.Validate();
            var photos = new Dictionary<string, string>();
            var orderToInsert = _mapper.Map<Orders>(request);
            foreach (var orderItemRequest in request.OrderItems)
            {
                var orderItem = _mapper.Map<OrderItem>(orderItemRequest);
                photos.Add(orderItem.PhotoId, orderItemRequest.ImageBase64);
                orderToInsert.OrderItems.Add(orderItem);
            }
            var response = await InsertAsync(orderToInsert);
            if (!response)
            {
                throw new Exception($"Order could not be created");
            }
            foreach (var x in photos)
            {
                var insertedOrder = await GetByIdAsync(orderToInsert.Id);
                var fileBytes = Convert.FromBase64String(x.Value);
                await InsertAttachment(insertedOrder.Id, x.Key, fileBytes, insertedOrder.Rev);
            }
        }

        public async Task<OrderOverviewListResponse> GetOrdersOverview(int page, OrderStatus status)
        {
            var dbResult = await client.GetAsync($"{typeof(Orders).Name.ToLower()}" +
                                                 $"/{Views.orderByStatus}?key={(int)status}&include_docs=true");
            if (dbResult != null)
            {
                var orders = (JsonConvert.DeserializeObject<BaseViewEntity<Orders>>(await dbResult.Content.ReadAsStringAsync())).Rows.Select(x => x.Doc).OrderByDescending(x => x.CreatedOn).ToList();
                var response = new OrderOverviewListResponse
                {
                    TotalEntities = orders.Count,
                    Offset = (page - 1) * Numeric.ItemsPerPage,
                    Orders = _mapper.Map<List<OrderOverviewResponse>>(orders.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
                };
                foreach (var order in response.Orders)
                {
                    foreach (var item in order.OrderItems)
                    {
                        var photo = await GetAttachemntContent(order.OrderId, item.PhotoId);
                        item.ImageBase64 = photo != null ? Convert.ToBase64String(photo) : null;
                    }
                }
                return response;
            }
            return null;
        }

        public async Task ConfirmOrder(OrderChangeStatusRequest request)
        {
            request.Validate();
            var order = await GetByIdAsync(request.OrderId);
            order.OrderStatus = (int)OrderStatus.Approved;
            var orderItemsPerUser = order.OrderItems.GroupBy(x => x.SellerId);
            if (!await UpdateAsync(order))
            {
                throw new Exception($"Order could not be confirmed");
            }
            foreach (var item in orderItemsPerUser)
            {
                await _sellerService.AddRevenue(new SellerRevenue
                {
                    UserId = item.Key,
                    ProductIds = item.GroupBy(x => x.ProductId)
                    .Select(group =>
                    (Quantity: group.Sum(x => x.Quantity),
                      ProductId: group.Key)).ToList()
                });
            }
        }

        public async Task DeclineOrder(OrderChangeStatusRequest request)
        {
            request.Validate();
            var order = await GetByIdAsync(request.OrderId);
            order.OrderStatus = (int)OrderStatus.Declined;
            order.DeclineReason = request.Reason;
            if (string.IsNullOrWhiteSpace(request.Reason))
            {
                throw new InvalidRequestException($"Reason is required.");
            }
            if (!await UpdateAsync(order))
            {
                throw new Exception($"Order could not be declined");
            }
        }

        public async Task SentOrder(OrderChangeStatusRequest request)
        {
            request.Validate();
            var order = await GetByIdAsync(request.OrderId);
            order.OrderStatus = (int)OrderStatus.Sent;
            order.DeclineReason = request.Reason;
            if (!await UpdateAsync(order))
            {
                throw new Exception($"Order could not be sent");
            }
        }

        public async Task DeliveredOrder(OrderChangeStatusRequest request)
        {
            request.Validate();
            var order = await GetByIdAsync(request.OrderId);
            order.OrderStatus = (int)OrderStatus.Delivered;
            order.DeclineReason = request.Reason;
            if (!await UpdateAsync(order))
            {
                throw new Exception($"Order could not be delivered");
            }
        }

        public async Task<OrderUserOrdersListResponse> GetUserOrders(UserOrdersRequest request)
        {
            var dbResult = await client.GetAsync($"{typeof(Orders).Name.ToLower()}" +
                                                $"/{Views.ordersPerBuyer}?key=\"{request.UserId}\"&include_docs=true");
            if (dbResult != null)
            {
                var orders = JsonConvert.DeserializeObject<BaseViewEntity<Orders>>(await dbResult.Content.ReadAsStringAsync());
                var productDictionary = orders.Rows.OrderByDescending(x => x.Doc.CreatedOn)
                    .Skip((request.Page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage).ToDictionary(x => x.Value, x => _mapper.Map<Orders>(x.Doc));
                var orderItems = productDictionary.Select(x =>
                {
                    var orderItem = x.Value.OrderItems.FirstOrDefault(o => o.Id.Equals(x.Key));
                    return new OrderUserOrdersResponse()
                    {
                        Id = orderItem.Id,
                        OrderId = x.Value.Id,
                        ProductId = orderItem.ProductId,
                        SellerId = orderItem.SellerId,
                        CustomerId = x.Value.BuyerUsername,
                        PhotoId = orderItem.PhotoId,
                        ProductName = orderItem.ProductName,
                        Quantity = orderItem.Quantity,
                        Price = orderItem.Price,
                        Rated = orderItem.Rated,
                        OrderStatus = x.Value.OrderStatus,
                        DeclineReason = x.Value.DeclineReason,
                        OrderStatusString = ((OrderStatus)x.Value.OrderStatus).GetStringValue(),
                        SelectedOptions = _mapper.Map<List<OrderItemOptionResponse>>(orderItem.Options)
                    };
                }).ToList();
                foreach (var item in orderItems)
                {
                    var photo = await GetAttachemntContent(item.OrderId, item.PhotoId);
                    item.ImageBase64 = photo != null ? Convert.ToBase64String(photo) : null;

                }
                return new OrderUserOrdersListResponse()
                {
                    TotalEntities = orders.Rows.Count,
                    Offset = (request.Page - 1) * Numeric.ItemsPerPage,
                    Orders = orderItems
                };
            }
            return null;
        }

        public async Task RateProduct(OrderRatingRequest request)
        {
            request.Validate();
            var orderToUpdate = await GetByIdAsync(request.OrderId);
            var productTpUpdate = orderToUpdate.OrderItems.Where(x => x.Id == request.ItemId).FirstOrDefault();
            await _sellerService.RateUser(new SellerRatingRequest
            {
                UserId = request.UserId,
                SellerId = request.SellerId,
                Rating = request.SellerRating
            });
            await _productService.RateProduct(new ProductRatingRequest()
            {
                UserId = request.UserId,
                ProductId = productTpUpdate.ProductId,
                Rating = request.ProductRating,
                Comment = request.ProductComment
            });
            productTpUpdate.Rated = true;
            await UpdateAsync(orderToUpdate);
        }

        public async Task<int> GetActiveOrderCount(string userId)
        {
            var dbResult = await client.GetAsync($"{typeof(Orders).Name.ToLower()}" +
                                                $"/{Views.ordersPerBuyer}?key=\"{userId}\"&include_docs=true");
            if (dbResult != null)
            {
                var orders = JsonConvert.DeserializeObject<BaseViewEntity<Orders>>(await dbResult.Content.ReadAsStringAsync()).Rows;
                return orders.Where(x => x.Doc.OrderStatus != (int)OrderStatus.Declined && x.Doc.OrderStatus != (int)OrderStatus.Delivered).GroupBy(x => x.Doc.Id).ToList().Count;
            }
            return 0;
        }

    }
}
