using Data.DTOs;
using Data.DTOs.Menu;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Menus
{
    public interface IMenuService
    {
        ApiResponse<IList<MenuDto>> GetAll();

        ApiResponse<MenuDto> GetMenu(int id);
        ApiResponse<string> DeleteMenu(int id);
        ApiResponse<MenuDto> CreateMenu(MenuCreateDto menu, string path, IFormFile file);
        ApiResponse<MenuDto> EditMenu(MenuDto menu, string path, IFormFile file);
        ApiResponse<IList<MenuForDisplayDto>> GetMenusForDisplay();
        ApiResponse<IList<MenuForDisplayDto>> GetMenusByRestaurantId(int restaurantId);

    }
}
