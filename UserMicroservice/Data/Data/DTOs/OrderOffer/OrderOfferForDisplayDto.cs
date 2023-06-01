using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.OrderOffer
{
    public class OrderOfferForDisplayDto
    {
        public string Name { get; set; }

        public int? Quantity { get; set; }

        public float Price { get; set; }
    }
}
