using Business.Services.UserService;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotificationMicroservice.Controllers
{
    [Route("api/u/[controller]/[action]")]
    [ApiController]
    public class UserMicroserviceController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserMicroserviceController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            var userInDb = _userService.CreateUser(user);
            if (userInDb != null)
            {
                Console.WriteLine("--> Inbound POST # command Service");
            }
            else
            {
                Console.WriteLine("--> Something went wrong");
            }
            return Ok("Inbound test ok from UserMicroservice Controller");
        }
        [HttpPost]
        public IActionResult DeleteUser([FromBody] string userId)
        {
            _userService.DeleteUser(userId);
            return Ok("User was deleted successfully");
        }
    }
}
