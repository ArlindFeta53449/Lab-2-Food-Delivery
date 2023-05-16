using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MenuCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public int RestaurantId { get; set; }
        public string? ImagePath { get; set; }
    }
}
