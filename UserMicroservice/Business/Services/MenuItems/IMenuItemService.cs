using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.MenuItems
{
    public interface IMenuItemService
    {
        IList<MenuItemDto> GetAll();
        MenuItemDto GetMenuItem(int id);
        MenuItemDto EditMenuItem(MenuItemDto menuItem);
        MenuItemDto CreateMenuItem(MenuItemCreateDto menuItem);
        void DeleteMenuItem(int id);

        IList<MenuItemDto> SearchMenuItems(string menuitem);
        /*
        IList<MenuItemDto> GetMostPopularMenuItems(int count);
        */
    }
}
