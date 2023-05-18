using Data.DTOs.Menu;
using Data.DTOs.MenuItem;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Repositories.GenericRepository;
using Repositories.Repositories.MenusItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.MenusItems
{
    public class MenusItemRepository : Repository<MenuItem>, IMenusItemRepository
    {

        public MenusItemRepository(AppDbContext context) : base(context)
        {

        }

        public IList<MenuItem> SearchMenuItems(string menuitem)
        {
            var menuItems = Context.Set<MenuItem>().Where(m => m.Name.ToLower().Contains(menuitem.ToLower())).ToList();
            return menuItems;
        }
        public IList<MenuItemForDisplayDto> GetMenusIncludeMenus()
        {
            return Context.Set<MenuItem>().Include(x => x.Menu).Select(
                x => new MenuItemForDisplayDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    ImagePath = x.ImagePath,
                    Menu = x.Menu.Name,
                    Price = x.Price,
                    MenuId= x.MenuId
                }).ToList();
        }
        public IList<MenuItemForDisplayDto> GetMenuItemsByMenuId(int menuId)
        {
            return Context.Set<MenuItem>()
                .Include(x => x.Menu)
                .Where(x => x.MenuId == menuId)
                .Select(
                x => new MenuItemForDisplayDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                }).ToList();
        }
        public void RemoveMenuItemsByMenuId(int menuId)
        {
            var menuItems = Context.Set<MenuItem>().Where(x => x.MenuId == menuId).ToList();

            if (menuItems.Any())
            {
                Context.Set<MenuItem>().RemoveRange(menuItems);
                Context.SaveChanges();
            }
        }
        /*
        public IList<MenuItem> GetMostPopularMenuItems(int count)
        {
            var menuItems = Context.Set<MenuItem>()
                .OrderByDescending(m => m)
                .Take(count)
                .ToList();
            return menuItems;      
        }
        */
    }
}
