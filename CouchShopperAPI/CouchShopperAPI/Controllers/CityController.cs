using CouchShopper.Business.Interfaces;
using CouchShopper.Data.DTOs.Request.Common.Cities;
using CouchShopper.Data.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Route("city")]
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Route("city")]
        public async Task<IActionResult> GetCity([FromHeader] string id)
        {
            return Ok(await _cityService.GetCity(id));
        }

        [HttpGet]
        [Route("datagrid")]
        public async Task<IActionResult> GetCitiesDataGrid([FromQuery] int page, string countryId)
        {
            return Ok(await _cityService.GetActiveCities(page, countryId));
        }

        [HttpGet]
        [Route("dropdown")]
        public async Task<IActionResult> GetCitiesDropdown(string countryName)
        {
            return Ok(await _cityService.GetCitiesDropdown(countryName) ?? new List<DropdownModel>());
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CityAddRequest request)
        {
            await _cityService.CreateCity(request);

            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] CityUpdateRequest request)
        {
            await _cityService.UpdateCity(request);

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] CityDeleteRequest request)
        {
            await _cityService.DeleteCity(request);

            return Ok();
        }

    }
}
