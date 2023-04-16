using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }

        public IList<OrderItem> OrderItems { get; set; }

        public float Total { get; set; }

        public User User { get; set; }



        // public string PaymentIntentId { get; set; }

        // public string PaymentStatus { get; set; } // "Paid", "Pending", "Failed"

        // public string DeliveryAddress { get; set; }

    }
}
