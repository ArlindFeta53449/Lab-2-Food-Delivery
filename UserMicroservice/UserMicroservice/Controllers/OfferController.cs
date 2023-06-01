using Business.Services.Offers;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService; 
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OfferController(IOfferService offerService, IWebHostEnvironment webHostEnvironment)
        {
            _offerService = offerService;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public IActionResult GetAllOffers()
        {
            var response = _offerService.GetOffersForDisplay();
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public IActionResult GetOffer(int id)
        {
            var response = _offerService.GetOffer(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteOffer(int id)
        {
            var response = _offerService.DeleteOffer(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]

        public IActionResult CreateOffer(IFormFile files, [FromForm] OfferCreateDto offer, [FromForm] string menuItemOffersJson)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
            var response = _offerService.CreateOffer(offer, filePath, files, menuItemOffersJson);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]

        public IActionResult EditOffer(IFormFile? files, [FromForm] OfferDto offer)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
            var response = _offerService.EditOffer(offer, filePath, files);
            return StatusCode((int)response.StatusCode, response);
        }


    }
}
