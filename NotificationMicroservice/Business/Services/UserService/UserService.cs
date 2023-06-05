using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.UserService
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(
            IUserRepository userRepository,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User CreateUser(UserDto user)
        {

            return _userRepository.CreateUser(_mapper.Map<User>(user));
        }
        public bool UserExists(string externalId)
        {
            return _userRepository.UserExists(externalId);
        }
        public void DeleteUser(string userId)
        {
            _userRepository.DeleteUser(userId);
        }
    }
}
