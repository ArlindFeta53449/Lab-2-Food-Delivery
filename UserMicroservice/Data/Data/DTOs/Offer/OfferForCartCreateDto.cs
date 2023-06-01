using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Offer
{
    public class OfferForCartCreateDto
    {
        public int? Id { get; set; }
        public int? CartId { get; set; }
        public int OfferId { get; set; }

        public int Quantity { get; set; }
    }
}
