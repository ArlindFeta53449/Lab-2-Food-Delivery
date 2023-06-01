using Data.DTOs.OrderOffer;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.OrderOffers
{
    public interface IOrderOffersRepository: IRepository<OrderOffer>
    {
        IList<OrderOfferForDisplayDto> GetMenuItemsForOrderDisplay(int orderId);
    }
}
