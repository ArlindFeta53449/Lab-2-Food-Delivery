using Data.DTOs.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Order
{
    public class OrderForDisplayDto
    {
        public int Id { get; set; }

        public string User { get; set; }

        public IList<OrderItemForOrderDisplayDto> OrderItems { get; set; }

        public float Total { get; set; }
    }
}
