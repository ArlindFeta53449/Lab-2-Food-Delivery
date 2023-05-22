using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.MenuItem
{
    public class MenuItemForCartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string? ImagePath { get; set; }
        public int ? Quantity { get; set; }
    }
}
