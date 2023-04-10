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

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
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
        public IActionResult CreateRestaurant(RestaurantCreateDto restaurant)
        {
            var result = _restaurantService.CreateRestaurant(restaurant);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public IActionResult EditRestaurant(RestaurantDto restaurant)
        {
            var result = _restaurantService.EditRestaurant(restaurant);
            return Ok(result);
        }
 
    }

}