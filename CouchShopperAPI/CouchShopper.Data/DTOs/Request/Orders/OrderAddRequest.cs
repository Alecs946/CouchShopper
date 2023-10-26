using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Orders
{
    public class OrderAddRequest
    {
        public string ShippingType { get; set; }

        public double Price { get; set; }

        public string BuyerUsername { get; set; }

        public string BuyerFullName { get; set; }

        public string BuyerPhone { get; set; }

        public string ShippingAddress { get; set; }

        public double ShippingPrice { get; set; }

        public string PaymentMethodId { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; }
    }
}
