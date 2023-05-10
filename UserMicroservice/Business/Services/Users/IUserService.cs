using Data.DTOs.Authentification;
using Data.DTOs.Users;
using Data.Entities;
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
        string SignUp(UserCreateDto user);
        ApiResponse<UserLoginResponseDto> LogIn(UserLoginDto user);
        UserDto EditUser(UserEditDto user);
        bool DeleteUser(string id);
        bool ForgotPassword(ForgetPasswordDto forgetPassword);
        bool VerifyEmail(string token);

        bool ChangePassword(ChangePasswordDto changePassword);
        ForgotPasswordEmailResponseDTO SendForgotPasswordEmail(EmailSendDto email);

        IList<UserDto> GetAllUsersForAdminDashboardDisplay();

        UserEditDto GetUserByIdForEdit(string id);
    }
}
