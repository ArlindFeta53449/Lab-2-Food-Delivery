using Data.DTOs.Menu;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;
using Repository.Repositories.MenusItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Menus
{
    public class MenusRepository : Repository<Menu>, IMenusRepository
    {
        public MenusRepository(AppDbContext context) : base(context)
        {

        }
        public IList<MenuForDisplayDto> GetMenusIncludeRestaurants()
        {
            return Context.Set<Menu>().Include(x=>x.Restaurant).Select(
                x=>new MenuForDisplayDto() {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Image = x.Image,
                ImagePath = x.ImagePath,
                Restaurant = x.Restaurant.Name
                }).ToList();
        }
        public IList<MenuForDisplayDto> GetMenusByRestaurantId(int restaurantId)
        {
            return Context.Set<Menu>()
                .Include(x => x.Restaurant)
                .Where(x => x.Restaurant.Id == restaurantId)
                .Select(x => new MenuForDisplayDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = x.ImagePath
                })
                .ToList();
        }


    }
}
