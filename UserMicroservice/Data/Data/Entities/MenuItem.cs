using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public string ImagePath { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public IList<MenuItemOffer> MenuItemOffers { get; set; } = new List<MenuItemOffer>();
        public IList<CartMenuItem> CartMenuItems { get; set; } = new List<CartMenuItem>();
        public virtual IList<OrderMenuItem> OrderMenuItems { get; set; }

    }
}
