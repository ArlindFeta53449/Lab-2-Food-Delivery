using Business.Services.Token;
using Data.DTOs;
using Data.DTOs.Payment;
using Data.Entities;
using Data.Entities.StripeEntities;
using Serilog;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TokenService = Stripe.TokenService;

namespace Business.Services.Stripe.Contracts
{
    public class StripeService : IStripeService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;
        private readonly PaymentIntentService _paymentIntentService;

        public StripeService(
            ChargeService chargeService,
            CustomerService customerService,
            TokenService tokenService,
            PaymentIntentService paymentIntentService)
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
            _paymentIntentService = paymentIntentService;
        }
        public string AddStripeCustomer(StripeCustomer customer)
        {
            try
            {
                var options = new CustomerCreateOptions
                {
                    Email = customer.Email,
                    Name = customer.Name,
                    Source = customer.CardToken
                };
                var createdCustomer = _customerService.Create(options);
                return createdCustomer.Id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return "A problem occured while creating the customer's account in Stripe";
            }
        }

        public Payment AddStripePaymentIntent(PaymentDto payment)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = payment.Amount,
                Currency = payment.Currency,
                Description = payment.Description,
                Metadata = new Dictionary<string, string>
                {
                    { "DeliveryAddress", payment.DeliveryAddress },
             
                },
                PaymentMethod = payment.PaymentMethodId, 
                Customer = payment.StripeCustomerId 
            };
            var paymentIntent = _paymentIntentService.Create(options);

            return new Payment
            {
                Amount = paymentIntent.Amount,
                Currency = paymentIntent.Currency,
                Description = paymentIntent.Description,
                DeliveryAddress = payment.DeliveryAddress,
                StripeCustomerId = payment.StripeCustomerId,
                PaymentMethodId = payment.PaymentMethodId,
            };
        }
    }
}
