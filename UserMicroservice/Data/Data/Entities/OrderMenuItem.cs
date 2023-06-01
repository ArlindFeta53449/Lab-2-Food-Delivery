using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class OrderMenuItem
    {
        public int Id { get; set; }

        public int? MenuItemId { get; set; }

        public int? OrderId { get; set; }

        public int? Quantity { get; set; }

        public virtual MenuItem MenuItem { get; set; }

        public virtual Order Order { get; set; }
    }
}
