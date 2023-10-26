using CouchShopper.Data.DTOs.Response.Common.SpecialOffer;
using CouchShopper.Data.DTOs.Response.Products;
using CouchShopper.Data.DTOs.Response.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Common.Home
{
    public class HomePageResponse
    {
        public List<SpecialOfferResponse> SpecialOffers { get; set; }
        public List<ProductFeaturedProductsResponse> FeaturedProducts { get; set; }
        public List<ProductRecentAddResponse> RecentProducts { get; set; }
        public SellerByRatingResponse TopSeller { get; set; }
    }
}
