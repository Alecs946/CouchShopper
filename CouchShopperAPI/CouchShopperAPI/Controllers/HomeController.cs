using CouchShopper.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Route("home")]
    public class HomeController : BaseController
    {
        private readonly IHomeService _service;
        public HomeController(IHomeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("content")]
        public async Task<IActionResult> GetHomePageContent()
        {
            var response = await _service.GetHomePageContent();

            return Ok(response);
        }
    }
}
