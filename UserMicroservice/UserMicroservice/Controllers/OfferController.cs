using Business.Services.Offers;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }


        [HttpGet]
        public IActionResult GetAllOffers()
        {
            var offers = _offerService.GetAll();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public IActionResult GetOffer(int id)
        {
            var offers = _offerService.GetOffer(id);
            return Ok(offers);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteOffer(int id)
        {
            _offerService.DeleteOffer(id);
            return Ok();
        }

        [HttpPost]

        public IActionResult CreateOffer(OfferCreateDto offer)
        {
            var result = _offerService.CreateOffer(offer);
            return Ok(result);
        }

        [HttpPut]

        public IActionResult EditOffer(OfferDto offer)
        {
            var result = _offerService.EditOffer(offer);
            return Ok(result);
        }


    }
}
