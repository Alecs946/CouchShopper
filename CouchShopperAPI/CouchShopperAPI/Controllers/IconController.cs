using CouchShopper.Data.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CouchShopper.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CouchShopper.Data.DTOs.Request.Common.Icon;

namespace CouchShopper.API.Controllers
{
    [AllowAnonymous]
    [Route("icon")]
    public class IconController : BaseController
    {

        private readonly IIconService _service;
        public IconController(IIconService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("icon")]
        public async Task<IActionResult> GetIcon([FromHeader] string id)
        {
            var response = await _service.GetIcon(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("icon-datagrid")]
        public async Task<IActionResult> GetIconsDataGrid([FromQuery] int page)
        {
            var response = await _service.GetActiveIcons(page);

            return Ok(response);
        }

        [HttpGet]
        [Route("icon-dropdown")]
        public async Task<IActionResult> GetIconsDropdown()
        {
            var response = await _service.GetIconsDropdown();

            return Ok(response ?? new List<DropdownModel>());
        }

        [HttpPost]
        [Route("icon-create")]
        public async Task<IActionResult> Create([FromBody] IconAddRequest request)
        {
            await _service.CreateIcon(request);

            return Ok();
        }

        [HttpPut]
        [Route("icon-update")]
        public async Task<IActionResult> Update([FromBody] IconUpdateRequest request)
        {
            await _service.UpdateIcon(request);

            return Ok();
        }

        [HttpDelete]
        [Route("icon-delete")]
        public async Task<IActionResult> Delete([FromQuery] IconDeleteRequest request)
        {
            await _service.DeleteIcon(request);

            return Ok();
        }
    }
}
