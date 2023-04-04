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
            return Context.Set<User>().Find(id);
        }
        public User GetUserByEmail(string email)
        {
            return Context.Set<User>().FirstOrDefault(x => x.Email == email);
        }

    }
}
