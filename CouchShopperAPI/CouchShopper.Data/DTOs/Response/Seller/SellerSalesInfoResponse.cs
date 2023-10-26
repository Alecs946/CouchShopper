using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Seller
{
    public class SellerSalesInfoResponse
    {
        public List<SellerSalesDetails> SalesDetails { get; set; }
        public List<SellerSalesProducts> SalesProducts { get; set; }
    }
}
