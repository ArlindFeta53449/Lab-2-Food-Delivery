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

        Task SendForgotPasswordEmail(string userEmail);

        Task SendVerifyAccountEmail(UserCreateDto user);
    }
}
