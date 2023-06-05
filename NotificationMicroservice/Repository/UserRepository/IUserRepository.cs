using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        bool UserExists(string externalId);
        void DeleteUser(string userId);
    }
}
