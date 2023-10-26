using CouchShopper.Data.DTOs.Request.Products;
using System.Collections.Generic;

namespace CouchShopper.Data.DTOs.Request.Orders
{
    public class OrderItemRequest
    {

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public List<ProductCartOptionRequest> SelectedOptions { get; set; }

        public string SellerId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string ImageBase64 { get; set; }
    }
}
