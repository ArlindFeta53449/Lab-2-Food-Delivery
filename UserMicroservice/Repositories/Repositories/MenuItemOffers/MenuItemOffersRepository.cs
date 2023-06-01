using Data.Entities;
using Repositories.Repositories.GenericRepository;
using Repositories.Repositories.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.MenuItemOffers
{
    public class MenuItemOffersRepository : Repository<MenuItemOffer>, IMenuItemOffersRepository
    {
        public MenuItemOffersRepository(AppDbContext context) : base(context)
        {

        }

        public bool RemoveMenuItemOffersByOfferId(int offerId)
        {
            var menuItemOffers = Context.Set<MenuItemOffer>().Where(x=>x.OfferId == offerId).ToList();

            if(menuItemOffers.Any())
            {
                Context.Set<MenuItemOffer>().RemoveRange(menuItemOffers);
                Context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
