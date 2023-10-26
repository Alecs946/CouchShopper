using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    public class ProductCartResponseList
    {
        public double Subtotal { get; set; }
        public List<ProductCartResponse> CartItems { get; set; }
    }
}
