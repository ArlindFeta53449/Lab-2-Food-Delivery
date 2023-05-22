using Data.DTOs.MenuItem;
using Data.DTOs.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Cart
{
    public class CartCreateDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IList<MenuItemForCartCreateDto>? CartMenuItems { get; set; }
        public IList<OfferForCartCreateDto>? CartOffers { get; set; }
    }
}
