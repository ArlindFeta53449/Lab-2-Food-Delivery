using Data.DTOs.Users;
using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Users
{
    public interface IUserRepository:IRepository<User>
    {
        User GetUserByEmail(string email);

        User GetUserById(string id);

        User GetUserByVerificationToken(string token);
        User GetUserByEmailAndIsVerified(string email);

        IList<UserDto> GetAllUsersForAdminDashboardDisplay();

        UserEditDto GetUserByIdForEdit(string id);
        UserDto GetUserByIdIncludeRole(string id);
        UserForDashbooardDto GetUserStatisticsForDashboard();
        IList<string> GetAllAgentIds();
    }
}
