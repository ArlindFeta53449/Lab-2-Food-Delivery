﻿using Business.Services.MenuItems;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]

        public IActionResult GetAllMenuItem()
        {
            var menusItem = _menuItemService.GetAll();
            return Ok(menusItem);
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuItem(int id)
        {
            var menusItem = _menuItemService.GetMenuItem(id);
            return Ok(menusItem);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(int id)
        {
            _menuItemService.DeleteMenuItem(id);
            return Ok();
        }

        [HttpPost]

        public IActionResult CreateMenuItem(MenuItemCreateDto menuItem)
        {
            var result = _menuItemService.CreateMenuItem(menuItem);
            return Ok(result);
        }

        [HttpPut]

        public IActionResult EditMenuItem(MenuItemDto menuItem)
        {
            var result = _menuItemService.EditMenuItem(menuItem);
            return Ok(result);
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
