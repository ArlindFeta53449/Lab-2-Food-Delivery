using Data.DTOs.OrderMenuItem;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.OrderMenuItems
{
    public class OrderMenuItemsRepository: Repository<OrderMenuItem>, IOrderMenuItemsRepository
    {
        public OrderMenuItemsRepository(AppDbContext context) : base(context)
        {

        }

        public IList<OrderMenuItemForDisplayDto> GetMenuItemsForOrderDisplay(int orderId)
        {
            return Context.Set<OrderMenuItem>().Include(x => x.MenuItem).Where(x => x.OrderId == orderId)
                .Select(x => new OrderMenuItemForDisplayDto()
                {
                    Name = x.MenuItem.Name,
                    Quantity = x.Quantity,
                    Price = x.MenuItem.Price
                }).ToList();
        }
    }
}
