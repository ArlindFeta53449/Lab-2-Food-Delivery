using Business.Services.Users;
using Data.DTOs.Users;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetAllUsers() {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]

        public IActionResult GetUser(string id) {
        var user = _userService.GetUser(id);
            if(user !=null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult SignUp(UserCreateDto user)
        {
            var result = _userService.SignUp(user);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("There was a problem during sign up.");
            }
        }

        [HttpPut]
        public IActionResult EditUser(UserDto user) {
            var result = _userService.EditUser(user);
            if(result != null)

            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(string id)
        {
            var result = _userService.DeleteUser(id);
            if(result == true)
            {
                return Ok("The user was deleted successfuly");
            }
            else
            {
                return BadRequest($"Unable to delete user: {id}");
            }
        }
        [HttpPost]

        public IActionResult LogIn(UserLoginDto user)
        {
            var result = _userService.LogIn(user);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("The credentials are wrong");
            }
        }
        [HttpPut]
        public IActionResult ForgotPassword(ForgetPasswordDto forgotPassowrdDto) {
            var result = _userService.ForgotPassword(forgotPassowrdDto);
            if (result != null)

            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult VerifyEmail(string id)
        {
            var result = _userService.VerifyEmail(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult ChangePassword(ChangePasswordDto changePassword)
        {
            var result = _userService.ChangePassword(changePassword);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult SendForgotPasswordEmail(string email)
        {
            var result = _userService.SendForgotPasswordEmail(email);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
