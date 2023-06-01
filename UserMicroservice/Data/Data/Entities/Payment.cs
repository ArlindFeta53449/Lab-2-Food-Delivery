using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public long Amount { get; set; }
        public string Currency { get; set; }

        public string? Description { get; set; }
        public string DeliveryAddress { get; set; }
        public string StripeCustomerId { get; set; }
    }
}
