using Business.Services.Token;
using Data.Entities;
using Data.Entities.StripeEntities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public StripeService(
            ChargeService chargeService,
            CustomerService customerService,
            TokenService tokenService)
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
        }
        public async Task<Customer> AddStripeCustomerAsync(User customer, CancellationToken ct)
        {
            TokenCreateOptions tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = customer.Name,
                    Number = customer.CreditCard.CardNumber,
                    ExpYear = customer.CreditCard.ExpirationYear,
                    ExpMonth = customer.CreditCard.ExpirationMonth,
                    Cvc = customer.CreditCard.Cvc
                }
            };
            var stripeToken = await _tokenService.CreateAsync(tokenOptions, null, ct);
            CustomerCreateOptions customerOptions = new CustomerCreateOptions
            {
                Name = customer.Name,
                Email = customer.Email,
                Source = stripeToken.Id
            };
            Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);
            return createdCustomer;

        }

        public async Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct)
        {
            ChargeCreateOptions paymentOptions = new ChargeCreateOptions
            {
                Customer = payment.UserId,
                ReceiptEmail = payment.RecieptEmail,
                Description = payment.Description,
                Currency = payment.Currency,
                Amount = payment.Amount
            };

            var createdPayment = await _chargeService.CreateAsync(paymentOptions, null, ct);

            return new StripePayment()
            {
                Amount = payment.Amount,
                Currency = payment.Currency,
                CustomerId = createdPayment.CustomerId,
                Description = createdPayment.Description,
                ReceiptEmail = createdPayment.ReceiptEmail,
                PaymentId = createdPayment.Id
            };
        }
    }
}
