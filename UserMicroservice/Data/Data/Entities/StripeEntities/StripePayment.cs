﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.StripeEntities
{
    public class StripePayment
    {
       public string CustomerId { get; set; }
       public string ReceiptEmail { get; set; }
       public string Description { get; set; }
       public string Currency { get; set; }
       public long Amount { get; set; }
       public string PaymentId { get; set; }

    }
}
