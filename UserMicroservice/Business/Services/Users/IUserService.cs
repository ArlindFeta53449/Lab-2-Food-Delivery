using Data.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Users
{
    public interface IUserService
    {
        IList<UserDto> GetAll();
        UserDto GetUser(string id);
        UserDto SignUp(UserCreateDto user);
        UserLoginResponseDto LogIn(UserLoginDto user);
        UserDto EditUser(UserDto user);
        bool DeleteUser(string id);
        bool ForgotPassword(ForgetPasswordDto forgetPassword);
        bool VerifyEmail(string token);

        bool ChangePassword(ChangePasswordDto changePassword);
        string SendForgotPasswordEmail(string email);
    }
}
