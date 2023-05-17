using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.MenuItem
{
    public class MenuItemForDisplayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string? Image { get; set; }
        public string? ImagePath { get; set; }
        public string Menu { get; set; }
        public int MenuId { get; set; }
    }
}
