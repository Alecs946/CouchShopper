using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    public class ProductRecentAddResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public double Rating { get; set; }

        public int NumberOfSales { get; set; }

        public string PhotoId { get; set; }

        public ProductPhotoResponse Photo { get; set; }
    }
}
