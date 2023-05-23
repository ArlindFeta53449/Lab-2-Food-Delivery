using Data.Entities;
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
            var cartOffer = Context.Set<CartOffer>().Where(x => x.CartId == cartId && x.OfferId == offerId).FirstOrDefault();
            if (cartOffer != null)
            {
                return cartOffer;
            }
            return null;
        }
    }
}
