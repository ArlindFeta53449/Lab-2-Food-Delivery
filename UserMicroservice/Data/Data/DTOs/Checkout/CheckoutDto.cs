using Data.DTOs.Payment;
using Data.Entities.StripeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Checkout
{
    public class CheckoutDto
    {
        public string userId { get; set; }
        public StripeCustomer? StripeCustomer { get; set; }
        public PaymentDto PaymentIntent { get; set; }
        //public int? Amount { get; set; }
    }
}
