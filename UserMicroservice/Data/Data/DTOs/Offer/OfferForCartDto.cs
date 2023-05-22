using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Offer
{
    public class OfferForCartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public float Price { get; set; }
        public int DiscountPercent { get; set; }
        public int Quantity { get; set; }    
    }
}
