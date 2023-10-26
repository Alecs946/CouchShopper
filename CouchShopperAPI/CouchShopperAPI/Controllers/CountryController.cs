using CouchShopper.Business.Interfaces;
using CouchShopper.Data.DTOs.Request.Common.Country;
using CouchShopper.Data.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Route("country")]
    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [Route("country")]
        public async Task<IActionResult> GetCountry([FromHeader] string id)
        {
            return Ok(await _countryService.GetCountry(id));
        }

        [HttpGet]
        [Route("datagrid")]
        public async Task<IActionResult> GetCountriesDataGrid([FromQuery] int page)
        {
            return Ok(await _countryService.GetActiveCountries(page));
        }

        [HttpGet]
        [Route("dropdown")]
        public async Task<IActionResult> GetCountriesDropdown()
        {
            return Ok(await _countryService.GetCountriesDropdown() ?? new List<DropdownModel>());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CountryAddRequest request)
        {
            await _countryService.CreateCountry(request);

            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] CountryUpdateRequest request)
        {
            await _countryService.UpdateCountry(request);

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] CountryDeleteRequest request)
        {
            await _countryService.DeleteCountry(request);

            return Ok();
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("shipping-options")]
        public async Task<IActionResult> GetShippingOptions([FromQuery] string userId)
        {
            return Ok(await _countryService.GetShippingOptions(userId));
        }

        [HttpGet]
        [Route("country-shipping-options")]
        public async Task<IActionResult> GetCountryShippingOptions([FromQuery] string country)
        {
            return Ok(await _countryService.GetCountryShippingOptions(country));
        }
    }
}
