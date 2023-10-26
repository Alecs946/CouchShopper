using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Services;
using CouchShopper.Data.DTOs.Request.Orders;
using CouchShopper.Data.DTOs.Request.Products;
using CouchShopper.Data.DTOs.Request.Users;
using CouchShopper.Data.enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Route("order")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _service;
        
        public OrderController(IOrderService orderService)
        {
            _service = orderService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderAddRequest request)
        {
            await _service.CreateOrder(request);
            return Ok();
        }

        [HttpGet]
        [Route("order-created")]
        public async Task<IActionResult> OrderCreated([FromQuery] int page)
        {
            return Ok(await _service.GetOrdersOverview(page, OrderStatus.Created));
        }

        [HttpGet]
        [Route("order-approved")]
        public async Task<IActionResult> OrderApproved([FromQuery] int page)
        {
            return Ok(await _service.GetOrdersOverview(page, OrderStatus.Approved));
        }

        [HttpGet]
        [Route("order-declined")]
        public async Task<IActionResult> OrderDeclined([FromQuery] int page)
        {
            return Ok(await _service.GetOrdersOverview(page, OrderStatus.Declined));
        }
        
        [HttpGet]
        [Route("order-sent")]
        public async Task<IActionResult> OrderSent([FromQuery] int page)
        {
            return Ok(await _service.GetOrdersOverview(page, OrderStatus.Sent));
        } 
        
        [HttpGet]
        [Route("order-delivered")]
        public async Task<IActionResult> OrderDelivered([FromQuery] int page)
        {
            return Ok(await _service.GetOrdersOverview(page, OrderStatus.Delivered));
        }

        [HttpPut]
        [Route("order-confirm")]
        public async Task<IActionResult> ConfirmOrder([FromBody] OrderChangeStatusRequest request)
        {
            await _service.ConfirmOrder(request);
            return Ok();
        }

        [HttpPut]
        [Route("order-decline")]
        public async Task<IActionResult> DeclineOrder([FromBody] OrderChangeStatusRequest request)
        {
            await _service.DeclineOrder(request);
            return Ok();
        }

        [HttpPut]
        [Route("order-sent")]
        public async Task<IActionResult> SentOrder([FromBody] OrderChangeStatusRequest request)
        {
            await _service.SentOrder(request);
            return Ok();
        }
        
        [HttpPut]
        [Route("order-delivered")]
        public async Task<IActionResult> DeliveredOrder([FromBody] OrderChangeStatusRequest request)
        {
            await _service.DeliveredOrder(request);
            return Ok();
        }

        [HttpGet]
        [Route("order-user-orders")]
        public async Task<IActionResult> GetUserOrders([FromQuery] UserOrdersRequest request)
        {
            return Ok(await _service.GetUserOrders(request));
        }
       
        [HttpPut]
        [Route("order-rating")]
        public async Task<IActionResult> RateProduct([FromBody] OrderRatingRequest request)
        {
            await _service.RateProduct(request);
            return Ok();
        }

        [HttpGet]
        [Route("order-active-orders-count")]
        public async Task<IActionResult> GetUserOrders([FromQuery] string userId)
        {
            return Ok(await _service.GetActiveOrderCount(userId));
        }
                
    }
}
