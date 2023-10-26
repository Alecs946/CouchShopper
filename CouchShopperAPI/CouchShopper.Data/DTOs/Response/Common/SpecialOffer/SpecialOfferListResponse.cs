using CouchShopper.Data.DTOs.Response.Common.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Common.SpecialOffer
{
    public class SpecialOfferListResponse
    {
        public int TotalEntities { get; set; }

        public int Offset { get; set; }

        public List<SpecialOfferResponse> SpecialOffers { get; set; }
    }
}
