using Data.Entities;
using Data.Entities.StripeEntities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Stripe.Contracts
{
    public interface IStripeService
    {
        Task<Customer> AddStripeCustomerAsync(User customer, CancellationToken ct);
        Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct);
    }
}
