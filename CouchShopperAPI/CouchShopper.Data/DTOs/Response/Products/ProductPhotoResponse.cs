using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    public class ProductPhotoResponse
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public bool IsDefault { get; set; }
    }
}
