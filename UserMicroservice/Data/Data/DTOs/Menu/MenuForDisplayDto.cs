using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Menu
{
    public class MenuForDisplayDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? ImagePath { get; set; }
        public string Restaurant { get; set; }

        public int RestaurantId { get; set; }
    }
}
