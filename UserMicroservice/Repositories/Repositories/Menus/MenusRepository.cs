using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.MenusItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Menus
{
    public class MenusRepository : Repository<Menu>, IMenusRepository
    {
        public MenusRepository(AppDbContext context) : base(context)
        {

        }
    }
}
