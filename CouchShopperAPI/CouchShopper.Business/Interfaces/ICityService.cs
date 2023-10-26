using CouchShopper.Data.DTOs.Request.Common.Cities;
using CouchShopper.Data.DTOs.Response.Common.City;
using CouchShopper.Data.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface ICityService
    {
        Task<CityResponse> GetCity(string id);

        Task<CityListResponse> GetActiveCities(int page, string countryId);

        Task CreateCity(CityAddRequest request);

        Task UpdateCity(CityUpdateRequest request);

        Task DeleteCity(CityDeleteRequest request);

        Task<List<DropdownModel>> GetCitiesDropdown(string countryName);

    }
}
