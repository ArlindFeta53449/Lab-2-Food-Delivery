using Data.DTOs.MenuItem;
using Data.DTOs.Offer;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.CartMenuItems;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.CartOffers
{
    public class CartOfferRepository : Repository<CartOffer>, ICartOfferRepository
    {
        public CartOfferRepository(AppDbContext context) : base(context)
        {

        }

        public CartOffer GetOfferInCart(int cartId, int offerId)
        {
            var cartOffer = Context.Set<CartOffer>().AsNoTracking().Where(x => x.CartId == cartId && x.OfferId == offerId).FirstOrDefault();
            if (cartOffer != null)
            {
                return cartOffer;
            }
            return null;
        }
        public IList<CartOffer> GetOffersInCartByCartId(int cartId)
        {
            var cartOffers = Context.Set<CartOffer>().AsNoTracking().Where(x => x.CartId == cartId ).ToList();
            if (cartOffers != null)
            {
                return cartOffers;
            }
            return null;
        }
        public int GetNumberOfOffersInCartByUserId(string userId)
        {
            return Context.Set<CartOffer>()
                          .Include(x => x.Cart)
                          .AsNoTracking()
                          .Where(x => x.Cart.UserId == userId)
                          .Count();
        }
        public IList<OfferForCheckoutDto> GetOffersForTotalCalculation(string userId)
        {
            return Context.Set<CartOffer>()
                           .Include(x => x.Cart)
                           .Include(x => x.Offer)
                           .AsNoTracking()
                           .Where(x => x.Cart.UserId == userId)
                           .Select(x => new OfferForCheckoutDto()
                           {
                               OfferId = x.OfferId,
                               Quantity = x.Quantity,
                               Price = x.Offer.Price
                           }).ToList();
        }
    }
}
