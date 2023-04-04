using AutoMapper;
using Business.Services.Token;
using Data.DTOs.Users;
using Data.Entities;
using Microsoft.IdentityModel.Tokens;
using Repositories.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Users
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
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
        public UserDto EditUser(UserDto user)
        {
            var userInDb = _userRepository.GetUserById(user.Id);
            if(userInDb != null)
            {
                _mapper.Map(user,userInDb);
                _userRepository.Update(userInDb);
                return _mapper.Map<UserDto>(userInDb);
            }
            else
            {
                return null;
            }
            
        }
        public bool DeleteUser(string id)
        {
            var user = _userRepository.GetUserById(id);
            if(user != null)
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
        public UserDto SignUp(UserCreateDto user)
        {
            var mappedUser = _mapper.Map<User>(user);
            mappedUser.Password = HashPassword(mappedUser.Password);
            if (_userRepository.Add(mappedUser))
            {
                return _mapper.Map<UserDto>(mappedUser);
            }
            else
            {
                return null;
            }
        }

        public UserLoginResponseDto LogIn(UserLoginDto user)
        {
            var userExists = _userRepository.GetUserByEmail(user.Email);
            if(userExists != null)
            {
                if (userExists.Password.Equals(HashPassword(user.Password)))
                {
                    return new UserLoginResponseDto()
                    {
                        Id = userExists.Id,
                        RoleId = userExists.RoleId,
                        Email = userExists.Email,
                        Token = _tokenService.CreateToken(userExists)
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
