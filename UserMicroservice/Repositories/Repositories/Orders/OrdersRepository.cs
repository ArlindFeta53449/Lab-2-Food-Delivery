using Data.DTOs;
using Data.DTOs.Cart;
using Data.DTOs.Order;
using Data.DTOs.Payment;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Orders
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    { 
        public OrdersRepository(AppDbContext context) : base(context)
        {
        }
        public IList<OrderForDisplayDto> getAllOrdersWithOrderItems()
        {
            return Context.Set<Order>().Include(x=>x.User)
                .Select(x => new OrderForDisplayDto()
                {
                    Id = x.Id,
                    User = x.User.Name + " " + x.User.Surname,
                    Total = x.Total,


                })
                .ToList();
        }

        public IList<Order> GetTopSellingOrders()
        {
            var topSellingOrders = Context.Set<Order>()
                .Include(o => o.OrderMenuItems)
                    .ThenInclude(omi => omi.MenuItem)
                .OrderByDescending(o => o.OrderMenuItems.Sum(omi => omi.Quantity))
                .Take(10)
                .ToList();

            return topSellingOrders;
        }

        /* public void CreateOrder(CartForOrderDto cart,long amount)
         {
             var order = new OrderCreateDto
             {
                 UserId = cart.UserId,
                 OrderMenuItems = cart.OrderMenuItems,
                 OrderOffers = cart.OrderOffers,
                 Total = amount
             };
             var mappedOrder = _mapper.Map<Order>(order);
             Context.Set<Order>().Add(mappedOrder);
         }*/
    }
}
