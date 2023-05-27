using Data.DTOs.MenuItem;
using Data.DTOs.Offer;
using Data.DTOs.OrderOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Cart
{
    public class CartForOrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IList<MenuItemForOrderCreateDto> OrderMenuItems { get; set; }
        public IList<OrderOfferCreateDto> OrderOffers { get; set; }
    }
}
