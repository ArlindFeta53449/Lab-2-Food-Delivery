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
        public string ImagePath { get; set; }
        public int RestaurantId { get; set; }
        public float Price { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Restaurant Restaurant { get; set; }
        public virtual IList<MenuItemOffer> MenuItemOffers { get; set; }
        public virtual IList<CartOffer> CartOffers { get; set;}
        public virtual IList<OrderOffer> OrderOffers { get; set; }
    }
}
