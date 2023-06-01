using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.MenuItem
{
    public class MenuItemForCartCreateDto
    {
        public int? Id { get; set; }
        public int? CartId { get; set; }
        public int MenuItemId { get; set; }

        public int Quantity { get; set; }
    }
}
