using CouchShopper.Data.DTOs.Request.Orders;
using CouchShopper.Data.DTOs.Response.Order;
using CouchShopper.Data.DTOs.Response.Users;
using CouchShopper.Data.enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(OrderAddRequest request);

        Task<OrderOverviewListResponse> GetOrdersOverview(int page, OrderStatus status);

        Task ConfirmOrder(OrderChangeStatusRequest request);

        Task DeclineOrder(OrderChangeStatusRequest request);

        Task SentOrder(OrderChangeStatusRequest request);

        Task DeliveredOrder(OrderChangeStatusRequest request);

        Task<OrderUserOrdersListResponse> GetUserOrders(UserOrdersRequest request);

        Task RateProduct(OrderRatingRequest request);

        Task<int> GetActiveOrderCount(string userId);
    }
}
