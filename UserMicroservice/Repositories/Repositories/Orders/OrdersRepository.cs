using Data.DTOs;
using Data.DTOs.Order;
using Data.DTOs.OrderItem;
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
            return Context.Set<Order>().Include(x => x.OrderItems).Include(x=>x.User)
                .Select(x => new OrderForDisplayDto()
                {
                    Id = x.Id,
                    User = x.User.Name + " " + x.User.Surname,
                    OrderItems = x.OrderItems.Select(y => new OrderItemForOrderDisplayDto()
                    {
                        OrderItemName = y.MenuItem.Name,
                        Quantity = y.Quantity
                    }).ToList(),
                    Total = x.Total,


                })
                .ToList();
        }
        
    }
}
