using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    public class ProductRangeResponse
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string SellerId { get; set; }

        public string PhotoId { get; set; }

        public string ImageBase64 { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

    }
}
