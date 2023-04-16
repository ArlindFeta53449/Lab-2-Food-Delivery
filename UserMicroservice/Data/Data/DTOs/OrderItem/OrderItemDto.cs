using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.OrderItem
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }
    }
}
