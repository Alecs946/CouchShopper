using CouchShopper.Business.Interfaces;
using CouchShopper.Data.DTOs.Request.Common.SpecialOffer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CouchShopper.API.Controllers
{
    [Route("special-offer")]
    public class SecialOfferController : BaseController
    {
        private readonly ISpecialOfferService _specialOfferService;

        public SecialOfferController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [HttpGet]
        [Route("offer")]
        public async Task<IActionResult> GetSpecialOffer([FromQuery] string id)
        {
            var response = await _specialOfferService.GetSpecialOffer(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("offers")]
        public async Task<IActionResult> GetSpecialOffers()
        {
            var response = await _specialOfferService.GetSpecialOffers();

            return Ok(response);
        }

        [HttpGet]
        [Route("datagrid")]
        public async Task<IActionResult> GetSpecialOffersDataGrid([FromQuery] int page)
        {
            var response = await _specialOfferService.GetActiveSpecialOffers(page);

            return Ok(response);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] SpecialOfferAddRequest request)
        {
            await _specialOfferService.CreateSpecialOffer(request);

            return Ok();
        }

        [HttpPut]
        [Route("publish-unpublish")]
        public async Task<IActionResult> Publish([FromBody] SpecialOfferPublishUnpublishReques reques)
        {
            await _specialOfferService.PublishUpublishSpecialOffer(reques.OfferId);

            return Ok();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] SpecialOfferUpdateRequest request)
        {
            await _specialOfferService.UpdateSpecialOffer(request);

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] SpecialOfferDeleteRequest request)
        {
            await _specialOfferService.DeleteSpecialOffer(request);

            return Ok();
        }
    }
}
