using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Products
{
    public class ProductAddRequest
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Price { get; set; }

        public double? SalePrice { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }

        public string ProductDescription { get; set; }

        public bool FeatureProduct { get; set; }

        public List<ProductOptionsRequest> Options { get; set; }

        public List<ProductPhotoRequest> Photos { get; set; }

    }
}
