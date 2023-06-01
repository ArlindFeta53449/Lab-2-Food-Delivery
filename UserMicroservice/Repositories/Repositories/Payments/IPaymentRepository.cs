using Data.DTOs.Payment;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Payments
{
    public interface IPaymentRepository: IRepository<Payment>
    {
        bool CreatePayment(PaymentIntentResponse paymentIntentResponse);
    }
}
