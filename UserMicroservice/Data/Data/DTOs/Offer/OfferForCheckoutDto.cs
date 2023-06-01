using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Offer
{
    public class OfferForCheckoutDto
    {
        public int? OfferId { get; set; }

        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
