using Data.DTOs.Order;
using Data.DTOs.Users;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services._01_Mailing
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

        Task SendForgotPasswordEmail(User user, string token);

        Task SendVerifyAccountEmail(UserCreateDto user);
        Task SendOrderStatusEmailToCustomerAsync(User user, Order order, float distance);
        Task SendEmailToCustomerWhenOrderAcceptedAsync(User user, OrderForDisplayDto order);
        Task SendEmailToCustomerWhenOrderHasArrivedAsync(User user, OrderForDisplayDto order);
        Task SendEmailToCustomerWhenOrderOnItsWayAsync(User user, OrderForDisplayDto order);
    }
}
