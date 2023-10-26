using CouchShopper.Business.Interfaces;
using CouchShopper.Data.DTOs.Request.Common.Category;
using CouchShopper.Data.DTOs.Request.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Route("product")]
    public class ProductController : BaseController
    {
        private readonly IProductService _service;

        public ProductController(IProductService productService)
        {
            _service = productService;
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductAddRequest request)
        {
            return Ok(await _service.CreatePoduct(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] string id)
        {
            return Ok(await _service.GetProduct(id));
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetProductCount([FromQuery] string userId)
        {
            return Ok(await _service.GetProductCount(userId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest request)
        {
            await _service.UpdateProduct(request);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult>Delete([FromQuery] ProductDeleteRequest request)
        {
            await _service.DeleteProduct(request);
            return Ok();
        }

        [HttpGet]
        [Route("datagrid-per-user")]
        public async Task<IActionResult> GetProductPerUser([FromQuery] int page, [FromQuery] string userId)
        {
            var response = await _service.GetProductsPerUser(page, userId);

            return Ok(response);
        }

        [HttpGet]
        [Route("product-reviews")]
        public async Task<IActionResult> GetProductReviews([FromQuery] int page, [FromQuery] string productId)
        {
            var response = await _service.GetProductReviews(page, productId);

            return Ok(response);
        }

        [HttpPost]
        [Route("cart-items")]
        public async Task<IActionResult> GetCartItems([FromBody] List<ProductCartItemsRequest> request)
        {
            var response = await _service.GetCartItems(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("product-search")]
        public async Task<IActionResult> ProductSearch([FromQuery] string searchPhrase,string category, int page)
        {
            return Ok(await _service.ProductSearch(searchPhrase,category, page));
        }


    }
}
