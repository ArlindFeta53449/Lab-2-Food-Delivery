using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public string Image { get; set; }
        public int MenuId { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Menu Menu { get; set; }
    }
}
