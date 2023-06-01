using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.StripeEntities
{
    public class StripeCustomer
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string CardToken { get; set; }
    }
}
