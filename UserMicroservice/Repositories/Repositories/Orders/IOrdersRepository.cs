using Data.DTOs.Order;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Orders
{
    public interface IOrdersRepository : IRepository<Order>
    {
        IList<OrderForDisplayDto> GetAllOrders();

        IList<OrderForDisplayDto> GetTopSellingOrders();

        OrderForDisplayDto GetOrderById(int orderId);
        OrderForDisplayDto GetActiveOrderForAgent(string agentId);
    }
}
