using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DTOs.RabbitMQ
{
    public class OrderPublishedDto
    {
        public int Id { get; set; }

        public string OrderStatus { get;set; }

        public string UserId { get; set; }

        public string Event { get; set; }
    }
}
