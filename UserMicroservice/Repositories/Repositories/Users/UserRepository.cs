using Data.DTOs.Users;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Users
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }
        public User GetUserById(string id)
        {
            return Context.Set<User>().Include(x=>x.Role).Where(x=>x.Id == id).FirstOrDefault();
        }
        public User GetUserByEmail(string email)
        {
            return Context.Set<User>().Include(x=>x.Role).FirstOrDefault(x => x.Email == email);
        }
        public User GetUserByVerificationToken(string token)
        {
            return Context.Set<User>().FirstOrDefault(x => x.AccountVerificationToken == token);
        }
        public User GetUserByEmailAndIsVerified(string email)
        {
            return Context.Set<User>().Include(x => x.Role).Where(x => x.IsEmailVerified == true && x.Email == email).FirstOrDefault();
        }

        public IList<UserDto> GetAllUsersForAdminDashboardDisplay()
        {
            return Context.Set<User>().Include(x => x.Role)
                .Select(x => new UserDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    Role = x.Role.Name,
                    IsEmailVerified = x.IsEmailVerified,
                    StripeCustomerId = x.StripeCustomerId
                }).ToList();
        }
        public UserEditDto GetUserByIdForEdit(string id)
        {
            return Context.Set<User>().Include(x => x.Role)
                .Select(x => new UserEditDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    RoleId = x.RoleId,
                    IsEmailVerified = x.IsEmailVerified

                }).FirstOrDefault(x => x.Id == id);
        }
        public UserDto GetUserByIdIncludeRole(string id)
        {
            return Context.Set<User>().Include(x => x.Role)
                .Select(x => new UserDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    Role = x.Role.Name,
                    IsEmailVerified = x.IsEmailVerified,
                    StripeCustomerId = x.StripeCustomerId,
                    AgentHasOrder = x.AgentHasOrder
                }).FirstOrDefault(x => x.Id == id);
        }
        public UserForDashbooardDto GetUserStatisticsForDashboard()
        {
            var verifiedUsers = Context.Set<User>().Where(x => x.IsEmailVerified == true).Count();
            var unverifiedUsers = Context.Set<User>().Where(x => x.IsEmailVerified == false).Count();
            var totalUsers = Context.Set<User>().Count();

            return new UserForDashbooardDto()
            {
                VerifiedUsers = verifiedUsers,
                UnverifiedUsers = unverifiedUsers,
                TotalUsers = totalUsers
            };

        }
        public IList<string> GetAllAgentIds()
        {
            return Context.Set<User>().Include(x => x.Role).Where(x => x.Role.Name == "Agent" && x.IsEmailVerified == true).Select(x => x.Id).ToList();
        }
    }
}
