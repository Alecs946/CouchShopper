using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    public class ProductSearchResponseList
    {
        public int TotalEntities { get; set; }

        public int Offset { get; set; }

        public List<ProductSearchResponse> Products { get; set; }
    }
}
