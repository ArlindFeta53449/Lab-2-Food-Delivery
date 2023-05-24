using Data.DTOs.MenuItem;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.Carts;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Repositories.Repositories.CartMenuItems
{
    public class CartMenuItemRepository : Repository<CartMenuItem>, ICartMenuItemRepository
    {
        public CartMenuItemRepository(AppDbContext context) : base(context)
        {

        }

        public CartMenuItem GetMenuItemInCart(int cartId,int menuItemId)
        {
            var menuItem = Context.Set<CartMenuItem>().AsNoTracking().Where(x=>x.CartId == cartId && x.MenuItemId == menuItemId).FirstOrDefault();
            if (menuItem != null)
            {
                return menuItem;
            }
            return null;
        }
        public IList<CartMenuItem> GetMenuItemsInCartByCartId(int cartId)
        {
            var menuItems = Context.Set<CartMenuItem>().AsNoTracking().Where(x => x.CartId == cartId).ToList();
            if (menuItems != null)
            {
                return menuItems;
            }
            return null;
        }
        public int GetNumberOfMenuItemsInCartByUserId(string userId)
        {
            return Context.Set<CartMenuItem>()
                          .Include(x=>x.Cart)
                          .AsNoTracking()
                          .Where(x => x.Cart.UserId == userId)
                          .Count();
        }
        public IList<MenuItemForCheckOutDto> GetMenuItemsForTotalCalculation(string userId)
        {
           return Context.Set<CartMenuItem>()
                          .Include(x => x.Cart)
                          .Include(x=>x.MenuItem)
                          .AsNoTracking()
                          .Where(x => x.Cart.UserId == userId)
                          .Select(x=> new MenuItemForCheckOutDto()
                          {
                              MenuItemId = x.MenuItemId,
                              Quantity = x.Quantity,
                              Price = x.MenuItem.Price
                          }).ToList();
        }
    }
}
