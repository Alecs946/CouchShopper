using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Seller
{
    public class SellerRatingRequest
    {
        public string UserId { get; set; }
        public string SellerId { get; set; }
        public int Rating { get; set; }
    }
}
