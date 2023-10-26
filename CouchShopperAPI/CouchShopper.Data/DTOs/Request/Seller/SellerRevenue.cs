using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Seller
{
    public class SellerRevenue
    {
        public string UserId { get; set; }
        public List<(int, string)> ProductIds { get; set; }
    }
}
