using Data.DTOs.MenuItem;
using Data.DTOs.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IList<MenuItemForCartDto> MenuItems { get; set; }
        public IList<OfferForCartDto> Offers { get; set; }
    }
}
