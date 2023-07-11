using Data.DTOs;
using Data.DTOs.Cart;
using Data.DTOs.Order;
using Data.DTOs.OrderMenuItem;
using Data.DTOs.OrderOffer;
using Data.DTOs.Payment;
using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;
using Repositories.Repositories.GenericRepository;
using Repositories.Repositories.OrderMenuItems;
using Repositories.Repositories.OrderOffers;
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
        private readonly IOrderMenuItemsRepository _orderMenuItemsRepository;
        private readonly IOrderOffersRepository _orderOffersRepository;
        public OrdersRepository(AppDbContext context,
            IOrderOffersRepository orderOffersRepository, 
            IOrderMenuItemsRepository orderMenuItemsRepository
            ) : base(context)
        {
            _orderMenuItemsRepository = orderMenuItemsRepository;
            _orderOffersRepository = orderOffersRepository;
        }
        public OrderForDisplayDto GetOrderById(int orderId)
        {
            if (orderId != null)
            {
                var menuItems = _orderMenuItemsRepository.GetMenuItemsForOrderDisplay(orderId);
                var offers = _orderOffersRepository.GetMenuItemsForOrderDisplay(orderId);
                return Context.Set<Order>().Include(x => x.User).Where(x => x.Id == orderId )
                    .Select(x => new OrderForDisplayDto()
                    {
                        Id = orderId,
                        Total = x.Total / 100,
                        MenuItems = menuItems,
                        Offers = offers,
                        User = x.User.Name + " " + x.User.Surname,
                        DeliveryAddress = x.DeliveryAddress,
                        Agent = Context.Set<User>().Where(y=>y.Id == x.AgentId).Select(y=>y.Name + " " + y.Surname).FirstOrDefault(),
                        OrderStatus =x.OrderStatus,
                        IsDelivered = x.IsDelivered

                    }).FirstOrDefault();
            }
            return null;
        }
        public IList<OrderForDisplayDto> GetAllOrders()
        {
            return Context.Set<Order>()
                .Include(x => x.User)
                .Include(x => x.OrderOffers)
                .ThenInclude(x => x.Offer)
                .Include(x => x.OrderMenuItems)
                .ThenInclude(x => x.MenuItem)
                .Where(x=>x.IsDelivered == false)
                .Select(x => new OrderForDisplayDto()
                {
                    Id = x.Id,
                    Total = x.Total / 100,
                    MenuItems = Context.Set<OrderMenuItem>().Include(y => y.MenuItem).Where(y => y.OrderId == x.Id)
                                .Select(y => new OrderMenuItemForDisplayDto()
                                {
                                    Name = y.MenuItem.Name,
                                    Quantity = y.Quantity,
                                    Price = y.MenuItem.Price
                                }).ToList(),
                    Offers = Context.Set<OrderOffer>().Include(z => z.Offer).Where(z => z.OrderId == x.Id)
                                .Select(z => new OrderOfferForDisplayDto()
                                {
                                    Name = z.Offer.Name,
                                    Quantity = z.Quantity,
                                    Price = z.Offer.Price
                                }).ToList(),
                    User = x.User.Name + " " + x.User.Surname,
                    DeliveryAddress = x.DeliveryAddress,
                    Agent = Context.Set<User>().Where(b => b.Id == x.AgentId).Select(b => b.Name + " " + b.Surname).FirstOrDefault(),
                    OrderStatus = x.OrderStatus,
                    IsDelivered = x.IsDelivered

                }).ToList();
        }
        public OrderForDisplayDto GetActiveOrderForAgent(string agentId)
        {
            var order = Context.Set<Order>().Include(x => x.User)
                .Where(x => x.AgentId == agentId && (x.OrderStatus != OrderStatuses.OrderNotSelected && x.OrderStatus != OrderStatuses.OrderIsDelivered))
                    .Select(x => new OrderForDisplayDto()
                    {
                        Id = x.Id,
                        Total = x.Total / 100,
                        MenuItems = Context.Set<OrderMenuItem>().Include(y => y.MenuItem).Where(y => y.OrderId == x.Id)
                                .Select(y => new OrderMenuItemForDisplayDto()
                                {
                                    Name = y.MenuItem.Name,
                                    Quantity = y.Quantity,
                                    Price = y.MenuItem.Price
                                }).ToList(),
                        Offers = Context.Set<OrderOffer>().Include(z => z.Offer).Where(z => z.OrderId == x.Id)
                                .Select(z => new OrderOfferForDisplayDto()
                                {
                                    Name = z.Offer.Name,
                                    Quantity = z.Quantity,
                                    Price = z.Offer.Price
                                }).ToList(),
                        User = x.User.Name + " " + x.User.Surname,
                        DeliveryAddress = x.DeliveryAddress,
                        Agent = Context.Set<User>().Where(y => y.Id == x.AgentId).Select(y => y.Name + " " + y.Surname).FirstOrDefault(),
                        OrderStatus = x.OrderStatus,
                        IsDelivered = x.IsDelivered

                    }).FirstOrDefault();
            if (order == null)
            {
                return null;
            }
            else
            {
                return order;
            }
        }



        public IList<OrderForDisplayDto> GetTopSellingOrders()
        {
            var topSellingOrders = Context.Set<Order>()
                .Include(o => o.OrderMenuItems)
                    .ThenInclude(omi => omi.MenuItem)
                .OrderByDescending(o => o.OrderMenuItems.Sum(omi => omi.Quantity))
                .Take(10)
                .Select(o => new OrderForDisplayDto
                {
                    Id = o.Id,
                    Total = o.Total / 100,
                    MenuItems = o.OrderMenuItems.Select(omi => new OrderMenuItemForDisplayDto
                    {
                        Name = omi.MenuItem.Name,
                        Quantity = omi.Quantity,
                        Price = omi.MenuItem.Price
                    }).ToList(),
                    Offers = o.OrderOffers.Select(oo => new OrderOfferForDisplayDto
                    {
                        Name = oo.Offer.Name,
                        Quantity = oo.Quantity,
                        Price = oo.Offer.Price
                    }).ToList()
                })
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
