using Business.Services.Restaurants;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RestaurantController(IRestaurantService restaurantService, IWebHostEnvironment webHostEnvironment)
        {
            _restaurantService = restaurantService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetAllRestaurant()
        {
            var response = _restaurantService.GetAll();
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public IActionResult GetRestaurant(int id)
        {
            var response = _restaurantService.GetRestaurant(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            var response = _restaurantService.DeleteRestaurant(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public IActionResult CreateRestaurant(IFormFile files,[FromForm]RestaurantCreateDto restaurant)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath,"Files");
            var response = _restaurantService.CreateRestaurant(restaurant, filePath, files);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut]
        public IActionResult EditRestaurant(IFormFile? files,[FromForm]RestaurantDto restaurant)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
            var response = _restaurantService.EditRestaurant(restaurant,filePath,files);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        public IActionResult GetRestaurantsForSelect()
        {
            var response = _restaurantService.GetRestaurantsForSelect();
            return StatusCode((int)response.StatusCode, response);
        }
 
    }

}