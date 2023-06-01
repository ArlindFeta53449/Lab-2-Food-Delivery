using Data.DTOs.OrderMenuItem;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.OrderMenuItems
{
    public interface IOrderMenuItemsRepository:IRepository<OrderMenuItem>
    {
        IList<OrderMenuItemForDisplayDto> GetMenuItemsForOrderDisplay(int orderId);
    }
}
