using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class OfferDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int MenuId { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
