using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Payment
{
    public class PaymentDto
    {
        public int Amount { get; set; }
        public string Currency { get; set; }

        public string Description { get; set; }

        public string DeliveryAddress { get; set; }

        public string StripeCustomerId { get; set; }

        public string? PaymentMethod { get; set; }

        public string? PaymentDefaultSource { get; set;}
    }
}
