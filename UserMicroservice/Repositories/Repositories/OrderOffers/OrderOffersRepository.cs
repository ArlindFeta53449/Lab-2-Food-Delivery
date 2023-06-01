using Data.DTOs.OrderMenuItem;
using Data.DTOs.OrderOffer;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.OrderOffers
{
    public class OrderOffersRepository:Repository<OrderOffer>,IOrderOffersRepository
    {
        public OrderOffersRepository(AppDbContext context) : base(context)
        {

        }
        public IList<OrderOfferForDisplayDto> GetMenuItemsForOrderDisplay(int orderId)
        {
            return Context.Set<OrderOffer>().Include(x => x.Offer).Where(x => x.OrderId == orderId)
                .Select(x => new OrderOfferForDisplayDto()
                {
                    Name = x.Offer.Name,
                    Quantity = x.Quantity,
                    Price = x.Offer.Price
                }).ToList();
        }
    }
}
