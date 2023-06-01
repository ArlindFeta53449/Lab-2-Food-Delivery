using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.MenuItem
{
    public class MenuItemForCheckOutDto
    {
        public int? MenuItemId { get; set; }
        public int Quantity { get; set; }

        public float Price { get; set; }
    }
}
