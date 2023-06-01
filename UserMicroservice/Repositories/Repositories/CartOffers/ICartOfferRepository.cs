using Data.DTOs.Offer;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.CartOffers
{
    public interface ICartOfferRepository:IRepository<CartOffer>
    {
        CartOffer GetOfferInCart(int cartId, int offerId);
        IList<CartOffer> GetOffersInCartByCartId(int cartId);
        int GetNumberOfOffersInCartByUserId(string userId);
        IList<OfferForCheckoutDto> GetOffersForTotalCalculation(string userId);
    }
}
