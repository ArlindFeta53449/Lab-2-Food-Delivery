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

namespace Business.Services.Stripe
{
    public class StripeService : IStripeService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;
        private readonly PaymentIntentService _paymentIntentService;
        private readonly PaymentMethodService _paymentMethodService;
        public StripeService(
            ChargeService chargeService,
            CustomerService customerService,
            TokenService tokenService,
            PaymentIntentService paymentIntentService,
            PaymentMethodService paymentMethodService)
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
            _paymentIntentService = paymentIntentService;
            _paymentMethodService = paymentMethodService;
        }
        public Customer GetCardTokenForCustomer(string customerId) {
            try
            {
                var customer = _customerService.Get(customerId);
                if(customer == null)
                {
                    return null;
                }
                return customer;
               
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return null;
            }
        }
        public string AddStripeCustomer(StripeCustomer customer)
        {
            try
            {
                var options = new CustomerCreateOptions
                {
                    Email = customer.Email,
                    Name = customer.Name,
                   // Source = customer.CardToken
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
        public PaymentMethod CreatePaymentMethodId(string cardToken)
        {
            var options = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Token = cardToken
                }
            };
            return _paymentMethodService.Create(options);
        }
        public PaymentIntentResponse AddStripePaymentIntent(PaymentDto payment)
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
                Customer = payment.StripeCustomerId
            };

            if (payment.PaymentDefaultSource == null)
            {
                var paymentMethod = CreatePaymentMethodId(payment.PaymentMethod);

                if (paymentMethod != null)
                {
                    var attachOptions = new PaymentMethodAttachOptions
                    {
                        Customer = payment.StripeCustomerId,
                    };
                    _paymentMethodService.Attach(paymentMethod.Id, attachOptions);
                    var updateOptions = new CustomerUpdateOptions
                    {
                        InvoiceSettings = new CustomerInvoiceSettingsOptions
                        {
                            DefaultPaymentMethod = paymentMethod.Id
                        }
                    };
                    _customerService.Update(payment.StripeCustomerId, updateOptions);

                    options.PaymentMethod = paymentMethod.Id;
                }
                else
                {
                    // Handle the case where payment method creation failed
                    // Return an error or handle it accordingly
                }
            }
            else
            {
                options.PaymentMethod = payment.PaymentDefaultSource;
            }

            var paymentIntent = _paymentIntentService.Create(options);

            return new PaymentIntentResponse
            {
                Amount = paymentIntent.Amount,
                Currency = paymentIntent.Currency,
                Description = paymentIntent.Description,
                DeliveryAddress = payment.DeliveryAddress,
                StripeCustomerId = payment.StripeCustomerId,
                CustomerSecret = paymentIntent.ClientSecret,
                PaymentIntentId = paymentIntent.Id
            };
        }
        public PaymentIntent ConfirmPayment(string paymentMethodId)
        {
            return _paymentIntentService.Confirm(paymentMethodId);
        }
    }
}
