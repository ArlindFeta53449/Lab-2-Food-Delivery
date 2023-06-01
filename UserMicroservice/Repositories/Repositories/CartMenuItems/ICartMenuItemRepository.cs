using Data.DTOs.MenuItem;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.CartMenuItems
{
    public interface ICartMenuItemRepository:IRepository<CartMenuItem>
    {
        CartMenuItem GetMenuItemInCart(int cartId, int menuItemId);
        IList<CartMenuItem> GetMenuItemsInCartByCartId(int cartId);
        int GetNumberOfMenuItemsInCartByUserId(string userId);
        IList<MenuItemForCheckOutDto> GetMenuItemsForTotalCalculation(string userId);
    }
}
