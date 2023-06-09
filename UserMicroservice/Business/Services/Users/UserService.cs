using AutoMapper;
using Business.Services._01_Mailing;
using Business.Services.Authentification;
using Business.Services.Token;
using Data.DTOs.Authentification;
using Data.DTOs.Users;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repositories.Repositories.Carts;
using Repositories.Repositories.Roles;
using Repositories.Repositories.Users;
using Serilog;
using System;
using System.Net;

using System.Security.Cryptography;
using System.Text;
using Business.Services.ZSyncDataServices.Http;
using Business.Services.XAsyncDataService;
using Data.DTOs.RabbitMQ;

namespace Business.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        private readonly IAuthentificationService _authentificationService;
        private readonly IRolesRepository _rolesRepository;
        private readonly ICartRepository _cartRepository;
        private readonly INotificationDataClient _notificationDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public UserService(IUserRepository userRepository, 
                            IMapper mapper, 
                            ITokenService tokenService, 
                            IMailService mailService, 
                            IAuthentificationService authentificationService,
                            ILogger<UserService> logger,
                            IRolesRepository rolesRepository,
                            ICartRepository cartRepository,
                            INotificationDataClient notificationDataClient,
                            IMessageBusClient messageBusClient
                )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _mailService = mailService;
            _authentificationService = authentificationService;
            _rolesRepository = rolesRepository;
            _cartRepository = cartRepository;
            _notificationDataClient = notificationDataClient;
            _messageBusClient = messageBusClient;
        }
        public ApiResponse<IList<UserDto>> GetAll()
        {
            try
            {
                var users = _userRepository.GetAll();
                return new ApiResponse<IList<UserDto>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<UserDto>>(users)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<UserDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<UserDto> GetUser(string id)
        {
            try { 
            var user = _userRepository.GetUserById(id);
                if(user == null)
                {
                    return new ApiResponse<UserDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The user does not exist" }
                    };
                }
                return new ApiResponse<UserDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<UserDto>(user)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<UserDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
         }
        public ApiResponse<UserDto> EditUser(UserEditDto user)
        {
            try
            {
                var userInDb = _userRepository.GetUserById(user.Id);
                if (userInDb == null)
                {
                    return new ApiResponse<UserDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The user does not exist" }
                    };
                }

                    _mapper.Map(user, userInDb);
                if (_userRepository.Update(userInDb))
                {
                    return new ApiResponse<UserDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<UserDto>(userInDb)
                    };
                }
                return new ApiResponse<UserDto>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "Something went wrong while updating the user" }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<UserDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }
        
        public ApiResponse<UserEditDto> GetUserByIdForEdit(string id)
        {
            try
            {
                var user = _userRepository.GetUserByIdForEdit(id);
                if (user == null)
                {
                    return new ApiResponse<UserEditDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The user does not exist" }
                    };
                }
                return new ApiResponse<UserEditDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<UserEditDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<string> ForgotPassword(ForgetPasswordDto forgetPassword)
        {
            try
            {
                var userInDb = _userRepository.GetUserById(forgetPassword.UserId);
                if (userInDb == null)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The user does not exist" }
                    };
                }

                 if (forgetPassword.NewPassword == forgetPassword.RepeatPassword)
                 {
                      userInDb.Password = HashPassword(forgetPassword.NewPassword);
                 if (_userRepository.Update(userInDb))
                 {
                     return new ApiResponse<string>() { 
                     StatusCode = HttpStatusCode.OK,
                     Message = "The password was changed successfully"
                     };
                 }
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Something went wrong while updating the data"
                    };
                    }
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The passwords do not match" }
                    };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<string> DeleteUser(string id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                if (user == null)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The user does not exist" }
                    };
                }
                if (_userRepository.Remove(user))
                {
                    _cartRepository.DeleteCart(id);
                    _notificationDataClient.DeleteUserInNotificationMicroservice(id);
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The user was deleted successfully"
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "Something went wrong while deleting the user" }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash of the byte array
                byte[] hash = sha256Hash.ComputeHash(bytes);

                // Convert the hash byte array to a string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
       
        public ApiResponse<UserForDashbooardDto> GetUserStatisticsForDashboard()
        {
            try
            {
                ;
                return new ApiResponse<UserForDashbooardDto>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _userRepository.GetUserStatisticsForDashboard()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<UserForDashbooardDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<string> SignUp(UserCreateDto user)
        {
            try
            {
                var mappedUser = _mapper.Map<User>(user);
                mappedUser.Password = HashPassword(mappedUser.Password);
                mappedUser.AccountVerificationToken = _tokenService.CreateVerifyAccountToken(user);
                user.AccountVerificationToken = mappedUser.AccountVerificationToken;
                mappedUser.RoleId = _rolesRepository.FindDefaultCustomerRole();
                if (_userRepository.Add(mappedUser))
                {
                    _cartRepository.CreateCart(mappedUser.Id);
                    _mailService.SendVerifyAccountEmail(user);
                    
                    return new ApiResponse<string>() { 
                    StatusCode = HttpStatusCode.OK,
                    Data = mappedUser.AccountVerificationToken
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "Something went wrong while registering the user" }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }
        public ApiResponse<ForgotPasswordEmailResponseDTO> SendForgotPasswordEmail(EmailSendDto email)
        {
            try
            {
                var userInDb = _userRepository.GetUserByEmail(email.Email);

                if (userInDb != null)
                {
                    var token = _tokenService.CreatePasswordToken(userInDb.Email);
                    var key = generateRandomKeyNumber();
                    var iv = generateRandomIvNumber();
                    var encryptedToken = _authentificationService.EncryptString(token, key, iv);
                    _mailService.SendForgotPasswordEmail(userInDb, token);

                    return new ApiResponse<ForgotPasswordEmailResponseDTO>() {
                    StatusCode = HttpStatusCode.OK,
                    Data = new ForgotPasswordEmailResponseDTO()
                                {
                                    EncryptedToken = encryptedToken,
                                    Key = key,
                                    Iv = iv,
                                    UserId = userInDb.Id,
                                }
                    };
                }
                return new ApiResponse<ForgotPasswordEmailResponseDTO>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "Something went wrong when sending the email" }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<ForgotPasswordEmailResponseDTO>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        private static byte[] generateRandomKeyNumber()
        {
            byte[] key = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            return key;

        }
        private static byte[] generateRandomIvNumber()
        {
            byte[] iv = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }

            return iv;
        }
        public ApiResponse<UserLoginResponseDto> LogIn(UserLoginDto user)
        {
            try
            {
                var userExists = _userRepository.GetUserByEmailAndIsVerified(user.Email);
                if (userExists != null && userExists.Password.Equals(HashPassword(user.Password)))
                {

                    return new ApiResponse<UserLoginResponseDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = new UserLoginResponseDto
                        {
                            Id = userExists.Id,
                            Role = userExists.Role.Name,
                            Email = userExists.Email,
                            Token = _tokenService.CreateToken(userExists),
                            StripeCustomerId = userExists.StripeCustomerId,
                            AgentHasOrder = userExists.AgentHasOrder
                        }

                    };
                }
                else
                {
                    return new ApiResponse<UserLoginResponseDto>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The credentials are wrong" }
                    };
                }
            }
            catch(Exception ex)
            {
                Log.Information("This is an informational message");
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<UserLoginResponseDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<string> VerifyEmail(string token)
        {
            try
            {
                var userInDb = _userRepository.GetUserByVerificationToken(token);
                if (userInDb != null)
                {
                    userInDb.IsEmailVerified = true;
                    userInDb.AccountVerificationToken = "";
                    if (_userRepository.Update(userInDb))
                    {
                        return new ApiResponse<string>()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "The email was verified successfully"
                        };
                    }
                    else
                    {
                        return new ApiResponse<string>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "The account was not verified" }
                        };
                    }
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "The account was not verified" }
                };
            }catch(Exception ex)
            {
                Log.Information("This is an informational message");
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
            
        }
        public ApiResponse<UserDto> GetCurrentUser(string token)
        {
            try
            {
                var userId = _tokenService.DecodeToken(token);
                var userInDb = _userRepository.GetUserByIdIncludeRole(userId);
                if (userInDb == null)
                {
                    return new ApiResponse<UserDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "Please log in first" }
                    };
                }
                return new ApiResponse<UserDto>() {
                    StatusCode = HttpStatusCode.OK,
                    Data = userInDb
                };

            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the ChangePassword request.");

                return new ApiResponse<UserDto>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string>() { "An error occurred while processing your request. Please try again later." }
                };
            }
           
        }
        
        public ApiResponse<string> ChangePassword(ChangePasswordDto changePassword)
        {
            try
            {
                var userInDb = _userRepository.GetUserById(changePassword.UserId);
                if (userInDb == null)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "Something went wrong" }
                    };
                }

                if (changePassword.NewPassword != changePassword.RepeatPassword)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The passwords do not match" }
                    };
                }

                if (userInDb.Password != HashPassword(changePassword.CurrentPassword))
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "Wrong current password" }
                    };
                }

                userInDb.Password = HashPassword(changePassword.NewPassword);
                if (_userRepository.Update(userInDb))
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The password was changed successfully"
                    };
                }

                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "The password was not verified" }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the ChangePassword request.");

                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string>() { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<IList<UserDto>> GetAllUsersForAdminDashboardDisplay()
        {
            try
            {
                var users = _userRepository.GetAllUsersForAdminDashboardDisplay();
                return new ApiResponse<IList<UserDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = users
                };
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the GetAllUsersForAdminDashboardDisplay request.");

                return new ApiResponse<IList<UserDto>> ()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string>() { "An error occurred while processing your request. Please try again later." }
                };
            } 
        }

    }
}
