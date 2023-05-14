using Data.DTOs.Users;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(User user);
        string CreatePasswordToken(string userEmail);
        string CreateVerifyAccountToken(UserCreateDto user);
        string DecodeToken(string token);
    }
}
