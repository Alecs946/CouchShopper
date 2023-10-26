using CouchShopper.Business.Interfaces;
using CouchShopper.Data.DTOs.Request.Common.Category;
using CouchShopper.Data.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [AllowAnonymous]
    [Route("category")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetCategory([FromHeader] string id)
        {
            var response = await _categoryService.GetCategory(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("datagrid")]
        public async Task<IActionResult> GetCategoriesDataGrid([FromQuery] int page)
        {
            var response = await _categoryService.GetActiveCategories(page);

            return Ok(response);
        }

        [HttpGet]
        [Route("dropdown")]
        public async Task<IActionResult> GetCategoriesDropdown()
        {
            var response = await _categoryService.GetCategoriesDropdown();

            return Ok(response ?? new List<DropdownModel>());
        }

        [HttpGet]
        [Route("extended-dropdown")]
        public async Task<IActionResult> GetCategoriesWithIcons()
        {
            var response = await _categoryService.GetCategoriesWithIcons();

            return Ok(response ?? new List<ExtendedDropDownModel>());
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CategoryAddRequest request)
        {
            await _categoryService.CreateCategory(request);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateRequest request)
        {
            await _categoryService.UpdateCategory(request);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] CategoryDeleteRequest request)
        {
            await _categoryService.DeleteCategory(request);

            return Ok();
        }
    }
}
