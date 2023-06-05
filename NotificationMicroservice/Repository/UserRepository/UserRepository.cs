
using Data.Entities;
using Data.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public class UserRepository:IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(INotificationDatabaseSettings settings, IMongoClient mongoClient)
        {

            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        public User CreateUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }
        public void DeleteUser(string userId)
        {
            _users.DeleteOne(x => x.ExternalId.Equals(userId));
        }
        public bool UserExists(string externalId)
        {
            var user = _users.Find(x => x.ExternalId == externalId).FirstOrDefault();
            if(user == null)
            {
                return false;
            }
            return true;
        }

    }
}
