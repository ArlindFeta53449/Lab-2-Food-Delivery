using Data.DTOs.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public IList<OrderOffer> OrderOffers { get; set; }
        public IList<OrderMenuItem> OrderMenuItems { get; set; }
        public float Total { get; set; }
        public string UserId { get; set; }
        public string? AgentId { get; set; }
        public string? OrderStatus { get; set; }
        public string DeliveryAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public User User { get; set; }

    }
}
