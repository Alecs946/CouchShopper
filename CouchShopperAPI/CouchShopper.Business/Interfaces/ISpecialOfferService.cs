using CouchShopper.Data.DTOs.Request.Common.SpecialOffer;
using CouchShopper.Data.DTOs.Response.Common.SpecialOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface ISpecialOfferService
    {
        Task<SpecialOfferResponse> GetSpecialOffer(string id);

        Task<List<SpecialOfferResponse>> GetSpecialOffers();

        Task<SpecialOfferListResponse> GetActiveSpecialOffers(int page);

        Task CreateSpecialOffer(SpecialOfferAddRequest request);

        Task PublishUpublishSpecialOffer(string offerId);

        Task UpdateSpecialOffer(SpecialOfferUpdateRequest request);

        Task DeleteSpecialOffer(SpecialOfferDeleteRequest request);
    }
}
