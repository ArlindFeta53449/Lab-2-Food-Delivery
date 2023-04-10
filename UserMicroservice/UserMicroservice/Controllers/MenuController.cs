using Business.Services.Menus;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Menu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]

        public IActionResult GetAllMenus()
        {
            var menus = _menuService.GetAll();
            return Ok(menus);
        }


        [HttpGet("{id}")]
        public IActionResult GetMenu(int id)
        {
            var menus = _menuService.GetMenu(id);
            return Ok(menus);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(int id)
        {
            _menuService.DeleteMenu(id);
            return Ok();
        }

        [HttpPost]

        public IActionResult CreateMenu(MenuCreateDto menu)
        {
            var result = _menuService.CreateMenu(menu);
            return Ok(result);
        }

        [HttpPut]

        public IActionResult EditMenu(MenuDto menu)
        {
            var result = _menuService.EditMenu(menu);
            return Ok(result);
        }

    }
}
