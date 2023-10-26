using CouchShopper.Data.DTOs.Request.Common.Country;
using CouchShopper.Data.DTOs.Response.Common.Country;
using CouchShopper.Data.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface ICountryService
    {
        Task<CountryResponse> GetCountry(string id);

        Task<CountryListResponse> GetActiveCountries(int page);

        Task CreateCountry(CountryAddRequest request);

        Task UpdateCountry(CountryUpdateRequest request);

        Task DeleteCountry(CountryDeleteRequest request);

        Task<List<DropdownModel>> GetCountriesDropdown();

        Task<List<ShippingOptionsResponse>> GetCountryShippingOptions(string country);

        Task<List<ShippingOptionsResponse>> GetShippingOptions(string userName);

    }
}
