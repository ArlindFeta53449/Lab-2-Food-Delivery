using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.RabbitMQ
{
    public class OrderCreatedDto
    {
        public int Id { get; set; }

        public string DeliveryAddress { get; set; }
        public float Total { get; set; }

        public IList<string> Agents { get; set; }
        public string Event { get; set; }
    }
}
