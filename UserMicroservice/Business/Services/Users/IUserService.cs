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
        ApiResponse<IList<UserDto>> GetAll();
        ApiResponse<UserDto> GetUser(string id);
        ApiResponse<string> SignUp(UserCreateDto user);
        ApiResponse<UserLoginResponseDto> LogIn(UserLoginDto user);
        ApiResponse<UserDto> EditUser(UserEditDto user);
        ApiResponse<string> DeleteUser(string id);
        ApiResponse<string> ForgotPassword(ForgetPasswordDto forgetPassword);
        ApiResponse<string> VerifyEmail(string token);
        ApiResponse<string> ChangePassword(ChangePasswordDto changePassword);
        ApiResponse<ForgotPasswordEmailResponseDTO> SendForgotPasswordEmail(EmailSendDto email);
        ApiResponse<IList<UserDto>> GetAllUsersForAdminDashboardDisplay();
        ApiResponse<UserEditDto> GetUserByIdForEdit(string id);
        ApiResponse<UserDto> GetCurrentUser(string token);

        ApiResponse<UserForDashbooardDto> GetUserStatisticsForDashboard();
    }
}
