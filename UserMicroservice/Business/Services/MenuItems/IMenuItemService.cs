using Data.DTOs;
using Data.DTOs.MenuItem;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.MenuItems
{
    public interface IMenuItemService
    {
        ApiResponse<IList<MenuItemDto>> GetAll();
        ApiResponse<IList<MenuItemForDisplayDto>> GetMenuItemsForDisplay();
        ApiResponse<MenuItemDto> GetMenuItem(int id);
        ApiResponse<MenuItemDto> EditMenuItem(MenuItemDto menuItem, string path, IFormFile file);
        ApiResponse<MenuItemDto> CreateMenuItem(MenuItemCreateDto menuItem, string path, IFormFile file);
        ApiResponse<string> DeleteMenuItem(int id);
        IList<MenuItemDto> SearchMenuItems(string menuitem);
        ApiResponse<IList<MenuItemForDisplayDto>> GetMenuItemsByMenuId(int menuId);
        /*
        IList<MenuItemDto> GetMostPopularMenuItems(int count);
        */
    }
}
