using Data.DTOs;
using Data.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Orders
{
    public interface IOrderService
    {
        OrderDto GetOrderById(int id);
        IEnumerable<OrderDto> GetAllOrders();
        IEnumerable<OrderDto> GetOrdersByCustomerId(string userId);
        bool CreateOrder(OrderCreateDto orderDto);
        bool UpdateOrder(OrderDto orderDto);
        bool DeleteOrder(int id);
        IList<OrderForDisplayDto> GetAllOrdersWithOrderItems();
    }
}
