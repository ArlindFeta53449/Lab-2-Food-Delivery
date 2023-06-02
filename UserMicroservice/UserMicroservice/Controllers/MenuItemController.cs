using Business.Services.MenuItems;
using Data.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuItemController(IMenuItemService menuItemService, IWebHostEnvironment webHostEnvironment)
        {
            _menuItemService = menuItemService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]

        public IActionResult GetAllMenuItem()
        {
            var response = _menuItemService.GetMenuItemsForDisplay();
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuItem(int id)
        {
            var response = _menuItemService.GetMenuItem(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenuItem(int id)
        {
            var response = _menuItemService.DeleteMenuItem(id);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]

        public IActionResult CreateMenuItem(IFormFile files, [FromForm] MenuItemCreateDto menuItem)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
            var response = _menuItemService.CreateMenuItem(menuItem,filePath,files);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]

        public IActionResult EditMenuItem(IFormFile? files, [FromForm] MenuItemDto menuItem)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
            var response = _menuItemService.EditMenuItem(menuItem,filePath,files);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]

        public IActionResult SearchMenuItems(string searchItem)
        {
            try
            {
                var menuItems = _menuItemService.SearchMenuItems(searchItem);
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{menuId}")]
        public IActionResult GetMenuItemsByMenuId(int menuId)
        {
            var response = _menuItemService.GetMenuItemsByMenuId(menuId);
            return StatusCode((int)response.StatusCode, response);
        }


        /*
        [HttpGet]
        public IActionResult<IList<MenuItemDto>> GetMostPopularMenuItems()
        {
            var popularMenuItems = _menuItemService.GetMostPopularMenuItems();
            return Ok(popularMenuItems);
        } 
        */
    }
}
