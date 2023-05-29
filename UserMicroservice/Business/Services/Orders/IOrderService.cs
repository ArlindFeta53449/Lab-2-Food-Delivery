using Data.DTOs;
using Data.DTOs.Order;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Orders
{
    public interface IOrderService
    {
        ApiResponse<OrderForDisplayDto> GetOrderById(int id);
        ApiResponse<IList<OrderForDisplayDto>> GetAllOrders();
        IEnumerable<OrderDto> GetOrdersByCustomerId(string userId);
        bool CreateOrder(OrderCreateDto orderDto);
        bool UpdateOrder(OrderDto orderDto);
        bool DeleteOrder(int id);
        IList<OrderDto> GetTopSellingOrders();
        ApiResponse<string> AcceptOrder(int orderId, string userId);
        ApiResponse<OrderForDisplayDto> GetActiveOrderForAgent(string agentId);

    }
}
