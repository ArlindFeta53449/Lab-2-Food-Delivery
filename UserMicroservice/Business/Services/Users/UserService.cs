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
using Repositories.Repositories.Users;
using Serilog;
using System;

using System.Net;

using System.Security.Cryptography;
using System.Text;


namespace Business.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        private readonly IAuthentificationService _authentificationService;



        public UserService(IUserRepository userRepository, 
                            IMapper mapper, 
                            ITokenService tokenService, 
                            IMailService mailService, 
                            IAuthentificationService authentificationService,
                            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _mailService = mailService;
            _authentificationService = authentificationService;

        }
        public IList<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<IList<UserDto>>(users);
        }

        public UserDto GetUser(string id)
        {
            var user = _userRepository.GetUserById(id);
            return _mapper.Map<UserDto>(user);
        }
        public UserDto EditUser(UserEditDto user)
        {
            var userInDb = _userRepository.GetUserById(user.Id);
            if (userInDb != null)
            {
                _mapper.Map(user, userInDb);
                _userRepository.Update(userInDb);
                return _mapper.Map<UserDto>(userInDb);
            }
            else
            {
                return null;
            }

        }
        
        public UserEditDto GetUserByIdForEdit(string id)
        {
            var user = _userRepository.GetUserByIdForEdit(id);
            if(user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public bool ForgotPassword(ForgetPasswordDto forgetPassword)
        {
            var userInDb = _userRepository.GetUserById(forgetPassword.UserId);
            if (userInDb != null)
            {
                if (forgetPassword.NewPassword == forgetPassword.RepeatPassword)
                {
                    userInDb.Password = HashPassword(forgetPassword.NewPassword);
                    return _userRepository.Update(userInDb);
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        public bool DeleteUser(string id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
            {
                if (_userRepository.Remove(user))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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
        public string SignUp(UserCreateDto user)
        {
            var mappedUser = _mapper.Map<User>(user);
            mappedUser.Password = HashPassword(mappedUser.Password);
            mappedUser.AccountVerificationToken = _tokenService.CreateVerifyAccountToken(user);
            user.AccountVerificationToken = mappedUser.AccountVerificationToken;
            if (_userRepository.Add(mappedUser))
            {
                _mailService.SendVerifyAccountEmail(user);
                return mappedUser.AccountVerificationToken;
            }
            else
            {
                return null;
            }
        }
        public ForgotPasswordEmailResponseDTO SendForgotPasswordEmail(EmailSendDto email)
        {
            var userInDb = _userRepository.GetUserByEmail(email.Email);

            if (userInDb != null)
            {
                var token = _tokenService.CreatePasswordToken(userInDb.Email);
                var key = generateRandomKeyNumber();
                var iv = generateRandomIvNumber();
                var encryptedToken = _authentificationService.EncryptString(token, key, iv);
                _mailService.SendForgotPasswordEmail(userInDb, token);

                return new
                ForgotPasswordEmailResponseDTO {
                    EncryptedToken = encryptedToken,
                    Key = key,
                    Iv = iv,
                    UserId = userInDb.Id,
                };

            }
            else
            {
                return null;
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
                            Token = _tokenService.CreateToken(userExists)
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
        public bool VerifyEmail(string token)
        {
            var userInDb = _userRepository.GetUserByVerificationToken(token);
            if (userInDb != null)
            {
                userInDb.IsEmailVerified = true;
                userInDb.AccountVerificationToken = "";
                if (_userRepository.Update(userInDb))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ChangePassword(ChangePasswordDto changePassword)
        {
            var userInDb = _userRepository.GetUserById(changePassword.UserId);
            if (userInDb != null)
            {
                if (changePassword.NewPassword == changePassword.RepeatPassword) {
                    if (userInDb.Password == HashPassword(changePassword.CurrentPassword))
                    {
                        userInDb.Password = HashPassword(changePassword.NewPassword);
                        var result = _userRepository.Update(userInDb);
                        return result;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public IList<UserDto> GetAllUsersForAdminDashboardDisplay()
        {
            return _userRepository.GetAllUsersForAdminDashboardDisplay();
        }

    }
}
