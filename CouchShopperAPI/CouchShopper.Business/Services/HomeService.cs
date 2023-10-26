using AutoMapper;
using CouchShopper.Business.Interfaces;
using CouchShopper.Data.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using CouchShopper.Data.DTOs.Response.Common.Home;

namespace CouchShopper.Business.Services
{
    public class HomeService : BaseService<Common>, IHomeService
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;
        private readonly ISpecialOfferService _specialOffer;
        public HomeService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper, IProductService productService, ISellerService sellerService, ISpecialOfferService specialOffer)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
            _productService = productService;
            _sellerService = sellerService;
            _specialOffer = specialOffer;
        }
        public async Task<HomePageResponse> GetHomePageContent()
        {
            var response = new HomePageResponse
            {
                SpecialOffers = await _specialOffer.GetSpecialOffers(),
                FeaturedProducts = (await _productService.GetFeaturedProducts(1)).Products,
                RecentProducts = await _productService.GetRecentProducts(9),
                TopSeller = await _sellerService.GetTopSeller()
            };
            return response;
        }
    }
}
