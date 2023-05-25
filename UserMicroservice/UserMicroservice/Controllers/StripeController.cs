
using Business.Services.Stripe;
using Data.Entities;
using Data.Entities.StripeEntities;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IStripeService _stripeService;

        public StripeController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }
       /* [HttpPost]
        public async Task<ActionResult<Customer>> AddStripeCustomer(
           [FromBody] User customer,
           CancellationToken ct)
        {
            Customer createdCustomer = await _stripeService.AddStripeCustomerAsync(
                customer,
                ct);

            return StatusCode(StatusCodes.Status200OK, createdCustomer);
        }
        [HttpPost]
        public async Task<ActionResult<StripePayment>> AddStripePayment(
            [FromBody] AddStripePayment payment,
            CancellationToken ct)
        {
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(
                payment,
                ct);

            return StatusCode(StatusCodes.Status200OK, createdPayment);
        }*/

    }
}
