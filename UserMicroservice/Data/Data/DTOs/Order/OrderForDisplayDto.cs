using Data.DTOs.OrderMenuItem;
using Data.DTOs.OrderOffer;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Order
{
    public class OrderForDisplayDto
    {
        public int Id { get; set; }

        public string User { get; set; }

        public IList<OrderMenuItemForDisplayDto> MenuItems { get; set; }

        public IList<OrderOfferForDisplayDto> Offers { get; set; }

        public float Total { get; set; }

        public string Agent { get; set; }
        public string? DeliveryAddress { get;set;}
        public bool IsDelivered { get; set; }
        public OrderStatuses? OrderStatus { get; set; }
    }
}
