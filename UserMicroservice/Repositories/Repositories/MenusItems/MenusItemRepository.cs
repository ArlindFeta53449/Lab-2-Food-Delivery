using Data.Entities;
using Repositories;
using Repositories.Repositories.GenericRepository;
using Repository.Repositories.Menus;
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
    }
}
