using Data.DTOs.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Order
{
    internal class OrderCreateDto
    {
        public string UserId { get; set; }

        public IList<OrderItemDto> OrderItems { get; set; }

        public float Total { get; set; }
    }
}
