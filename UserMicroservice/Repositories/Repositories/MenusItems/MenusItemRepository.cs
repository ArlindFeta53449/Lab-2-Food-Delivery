using Data.Entities;
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
