using Data.DTOs.Payment;
using Data.Entities;
using Data.Entities.StripeEntities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Stripe
{
    public interface IStripeService
    {
        string AddStripeCustomer(StripeCustomer customer);
        PaymentIntentResponse AddStripePaymentIntent(PaymentDto payment);
        Customer GetCardTokenForCustomer(string customerId);
        PaymentIntent ConfirmPayment(string paymentMethodId);
    }
}
