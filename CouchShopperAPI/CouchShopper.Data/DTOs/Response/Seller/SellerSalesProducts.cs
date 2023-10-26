using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Seller
{
    public class SellerSalesProducts
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Revenue { get; set; }
    }
}
