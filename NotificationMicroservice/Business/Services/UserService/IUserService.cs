using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.UserService
{
    public interface IUserService
    {
        bool UserExists(string externalId);
        User CreateUser(UserDto user);
        void DeleteUser(string userId);
    }
}
