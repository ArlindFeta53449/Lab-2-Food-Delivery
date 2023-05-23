using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.StripeEntities
{
    public class AddStripePayment
    {
        public string  UserId { get; set; }
        public string  RecieptEmail { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public long Amount { get; set; }
    }
}
