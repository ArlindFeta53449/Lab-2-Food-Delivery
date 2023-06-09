using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class OrderPublishedDto
    {
        public int Id { get; set; }

        public string OrderStatus { get; set; }

        public string UserId { get; set; }

        public string Event { get; set; }
    }
}
