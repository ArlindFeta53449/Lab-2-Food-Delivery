using Data.DTOs.Payment;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Payments
{
    public class PaymentRepository:Repository<Payment>,IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {

        }
        public bool CreatePayment(PaymentIntentResponse paymentIntentResponse)
        {
            var payment = new Payment()
            {
                Amount = paymentIntentResponse.Amount,
                DeliveryAddress = paymentIntentResponse.DeliveryAddress,    
                Description = paymentIntentResponse.Description,    
                Currency = paymentIntentResponse.Currency,  
                StripeCustomerId = paymentIntentResponse.StripeCustomerId
            };
            if (this.Add(payment))
            {
                return true;
            }
            return false;

        }
    }
}
