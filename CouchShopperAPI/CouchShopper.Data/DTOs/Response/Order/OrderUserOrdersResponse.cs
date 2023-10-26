using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Order
{
    public class OrderUserOrdersResponse
    {
        public string Id { get; set; }
        public string OrderId { get; set; }

        public string SellerId { get; set; }

        public string ProductId { get; set; }

        public string CustomerId { get; set; }

        public string PhotoId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public bool Rated { get; set; }

        public string ImageBase64 { get; set; }

        public int OrderStatus { get; set; }

        public string OrderStatusString { get; set; }

        public string DeclineReason { get; set; }

        public List<OrderItemOptionResponse> SelectedOptions { get; set; }
    }
}
