using System.Collections.Generic;

namespace CouchShopper.Data.DTOs.Response.Common.City
{
    public class CityListResponse
    {
        public int TotalEntities { get; set; }

        public int Offset { get; set; }

        public List<CityResponse> Cities { get; set; }
    }
}
