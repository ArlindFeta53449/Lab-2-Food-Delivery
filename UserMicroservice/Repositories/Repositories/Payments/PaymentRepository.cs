using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Payments
{
    public class PaymentRepository:Repository<Payment>,IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {

        }
    }
}
