using CouchShopper.Data.DTOs.Response.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Seller
{
    public class SellerByRatingResponse
    {
        public string Id { get; set; }

        public string SellerName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string MemberSince { get; set; }

        public string ImageBase64 { get; set; }

        public List<ProductShortOverviewResponse> Products { get; set; }

    }
}
