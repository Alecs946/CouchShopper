using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    public class ProductResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public string DefaultPhotoId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Price { get; set; }

        public double SalePrice { get; set; }

        public int Quantity { get; set; }

        public double Rating { get; set; }

        public int NumberOfReviews { get; set; }

        public string Category { get; set; }

        public string ProductDescription { get; set; }

        public bool FeatureProduct { get; set; }

        public List<ProductOptionsResponse> Options { get; set; }

        public List<ProductPhotoResponse> Photos { get; set; }
    }
}
