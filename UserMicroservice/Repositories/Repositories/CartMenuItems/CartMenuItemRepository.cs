using Data.Entities;
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
            var menuItem = Context.Set<CartMenuItem>().Where(x=>x.CartId == cartId && x.MenuItemId == menuItemId).FirstOrDefault();
            if (menuItem != null)
            {
                return menuItem;
            }
            return null;
        }

    }
}
