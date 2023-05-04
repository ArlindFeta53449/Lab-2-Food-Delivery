﻿using Data.Entities;
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

    }
}
