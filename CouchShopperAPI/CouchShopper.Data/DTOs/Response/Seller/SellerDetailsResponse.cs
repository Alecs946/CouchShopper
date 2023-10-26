using CouchShopper.Data.DTOs.Response.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Seller
{
    public class SellerDetailsResponse
    {
        public string SellerName { get; set; }

        public string MemberSince { get; set; }

        public string ImageBase64 { get; set; }

        public int TotalSales { get; set; }

        public double Rating { get; set; }

        public int NumberOfReviews { get; set; }

        public ProductByUserListResponse Products { get; set; }
    }
}
