using CouchShopper.Data.DTOs.Response.Products;
using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Order
{
    public class OrderOverviewResponse
    {
        public string OrderId { get; set; }

        public string ShippingType { get; set; }

        public string BuyerFullName { get; set; }

        public string BuyerPhone { get; set; }

        public string ShippingAddress { get; set; }

        public string PaymentMethodId { get; set; }

        public int OrderStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DeclineReason { get; set; }

        public List<OrderItemResponse> OrderItems { get; set; }

        
    }
}
