

using Data.DTOs.MenuItem;
using Data.DTOs.OrderMenuItem;
using Data.DTOs.OrderOffer;
using Data.Enums;

namespace Data.DTOs
{
    public class OrderCreateDto
    {
        public IList<OrderOfferCreateDto> OrderOffers { get; set; }
        public IList<MenuItemForOrderCreateDto> OrderMenuItems { get; set; }
        public string UserId { get; set; }
        public float Total { get; set; }
        public string DeliveryAddress { get; set; }

        public OrderStatuses OrderStatus { get; set; }
    }
}
