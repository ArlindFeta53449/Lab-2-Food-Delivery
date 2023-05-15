using Business.Services.Token;
using Business.Services.Users;
using Data.DTOs.Users;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var response = _userService.GetAll();
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]

        public IActionResult GetUser(string id) {
            var response = _userService.GetUser(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{id}")]

        public IActionResult GetUserForEdit(string id)
        {
            var response = _userService.GetUserByIdForEdit(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost]
        public IActionResult SignUp(UserCreateDto user)
        {
            var response = _userService.SignUp(user);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        public IActionResult EditUser(UserEditDto user) {
            var response = _userService.EditUser(user);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var response = _userService.DeleteUser(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost]
        public IActionResult LogIn(UserLoginDto user)
        {
            var response = _userService.LogIn(user);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        public IActionResult ForgotPassword(ForgetPasswordDto forgotPassowrdDto) {
            var response = _userService.ForgotPassword(forgotPassowrdDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{token}")]
        public IActionResult VerifyEmail(string token)
        {
            var response = _userService.VerifyEmail(token);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        public IActionResult ChangePassword(ChangePasswordDto changePassword)
        {
            var response = _userService.ChangePassword(changePassword);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public IActionResult SendForgotPasswordEmail(EmailSendDto email)
        {
            var response = _userService.SendForgotPasswordEmail(email);
            return StatusCode((int)response.StatusCode, response);

        }
        [HttpGet]
        public IActionResult GetAllUsersForAdminDashboardDisplay()
        {
            var response = _userService.GetAllUsersForAdminDashboardDisplay();
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost("{token}")]
        public IActionResult GetCurrentUser(string token)
        {
            var response = _userService.GetCurrentUser(token);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
