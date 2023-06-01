using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class CartMenuItem
    {
        public int Id { get; set; }

        public int? MenuItemId { get; set; }

        public int? CartId { get; set; }

        public int  Quantity { get; set;}

        public virtual MenuItem MenuItem { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
