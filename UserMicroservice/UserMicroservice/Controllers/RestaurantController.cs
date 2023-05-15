using Business.Services.Restaurants;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [Route("api/[controller]")]
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
            var restaurants = _restaurantService.GetAll();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public IActionResult GetRestaurant(int id)
        {
            var restaurants = _restaurantService.GetRestaurant(id);
            return Ok(restaurants);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            _restaurantService.DeleteRestaurant(id);
            return Ok("Restauranti u fshi me sukses");
        }

        [HttpPost]
        public IActionResult CreateRestaurant(IFormFile files,[FromForm]RestaurantCreateDto restaurant)
        {
            try
            {
                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath,"Files");
                var result = _restaurantService.CreateRestaurant(restaurant, filePath, files);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
        [HttpPut("{id}")]
        public IActionResult EditRestaurant(RestaurantDto restaurant)
        {
            var result = _restaurantService.EditRestaurant(restaurant);
            return Ok(result);
        }
 
    }

}