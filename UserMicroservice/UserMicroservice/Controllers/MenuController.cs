using Business.Services.Menus;
using Data.DTOs;
using Data.DTOs.Menu;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Menu.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuController(IMenuService menuService, IWebHostEnvironment webHostEnvironment)
        {
            _menuService = menuService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]

        public IActionResult GetAllMenus()
        {
            var response = _menuService.GetMenusForDisplay();
            return StatusCode((int)response.StatusCode, response);
        }


        [HttpGet("{id}")]
        public IActionResult GetMenu(int id)
        {
            var response = _menuService.GetMenu(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(int id)
        {
            var response = _menuService.DeleteMenu(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]

        public IActionResult CreateMenu(IFormFile files, [FromForm] MenuCreateDto menu)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
            var response = _menuService.CreateMenu(menu, filePath, files);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]

        public IActionResult EditMenu(IFormFile? files, [FromForm] MenuDto menu)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
            var response = _menuService.EditMenu(menu, filePath, files);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{restaurantId}")]
        public IActionResult GetMenusByRestaurantId(int restaurantId)
        {
            var response = _menuService.GetMenusByRestaurantId(restaurantId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
