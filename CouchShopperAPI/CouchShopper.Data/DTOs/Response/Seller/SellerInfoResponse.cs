using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Seller
{
    public class SellerInfoResponse
    {
        public string SellerName { get; set; }

        public string MemberSince { get; set; }

        public string ImageBase64 { get; set; }
        public double Rating { get; set; }

        public int NumberOfReviews { get; set; }
    }
}
