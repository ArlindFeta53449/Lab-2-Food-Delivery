using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.OrderItems
{
    public interface IOrderItemService
    {
        IList<OrderItemDto> GetAll();
        OrderItemDto GetOrderItem(int id);
        void DeleteOrderItem(int id);
        OrderItemDto CreateOrderItem(OrderItemCreateDto orderitem);
        OrderItemDto EditOrderItem(OrderItemDto orderitem);
    }
}
