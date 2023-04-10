using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.MenusItems
{
    public interface IMenusItemRepository : IRepository<MenuItem>
    {
        IList<MenuItem> SearchMenuItems(string menuitem);
    }
}
