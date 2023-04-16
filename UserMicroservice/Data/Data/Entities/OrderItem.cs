using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        public MenuItem MenuItem { get; set;}
    }
}
