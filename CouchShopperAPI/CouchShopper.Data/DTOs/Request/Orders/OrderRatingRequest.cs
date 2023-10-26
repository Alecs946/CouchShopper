using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Orders
{
    public class OrderRatingRequest
    {
        public string UserId { get; set; }

        public string ItemId { get; set; }
        
        public string OrderId { get; set; }

        public string SellerId { get; set; }

        public string ProductComment { get; set; }

        public int SellerRating { get; set; }

        public int ProductRating { get; set; }
    }
}
