using Data.DTOs;
using Data.Entities;
using Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.NotificationsRepository
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notifications;
        public NotificationRepository(INotificationDatabaseSettings settings, IMongoClient mongoClient) {

            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _notifications = database.GetCollection<Notification>("Notifications");
        }

        public Notification Create(Notification notification)
        {
            _notifications.InsertOne(notification);
            return notification;
        }
        public IList<NotificationForDisplayDto> GetNotifications(string userId)
        {
            var notifications = _notifications
                .Find(x => x.Users.Any(y => y.Id == userId))
                .SortByDescending(x => x.Created)
                .ToList();

            var notificationDtos = notifications.Select(notification => new NotificationForDisplayDto
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                Created = notification.Created,
                Link = notification.Link,
                UserId = userId,
                IsRead = notification.Users.Where(user => user.Id == userId).Select(user=>user.IsRead).SingleOrDefault()
            }).ToList();

            return notificationDtos;
        }

        public Notification GetNotificationById(string id,string userId)
        {
            return _notifications.Find(notification => notification.Id == id && notification.Users.Any(y=>y.Id == userId)).FirstOrDefault();
        }
        public async Task<bool> ClearAllNotificationsForUser(string userId)
        {
            var filter = Builders<Notification>.Filter.ElemMatch(x => x.Users, y => y.Id == userId);
            var update = Builders<Notification>.Update.PullFilter("Users", Builders<UserForNotificationDto>.Filter.Eq("Id", userId));

            var updateResult = await _notifications.UpdateManyAsync(filter, update);

            if (updateResult.ModifiedCount > 0)
            {
                // Check if any notifications have empty 'Users' array and remove them
                var emptyUsersFilter = Builders<Notification>.Filter.Eq("Users", new List<UserForNotificationDto>());
                await _notifications.DeleteManyAsync(emptyUsersFilter);

                return true;
            }

            return false;
        }
        public async Task<bool> MarkNotificationAsRead(string notificationId, string userId)
        {
            var notificationFilter = Builders<Notification>.Filter.Eq("_id", ObjectId.Parse(notificationId));
            var updateFilter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<Notification>.Update.Set("users.$[user].IsRead", true);
            var arrayFilters = new List<ArrayFilterDefinition>
        {
            new BsonDocumentArrayFilterDefinition<Notification>(new BsonDocument("user._id", userId))
        };
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };

            var updateResult = _notifications.UpdateOne(notificationFilter, update, updateOptions);



            return updateResult.ModifiedCount > 0;
            
        }



        public async Task<bool> MarkAllNotificationsAsRead(string userId)
        {
            var filter = Builders<Notification>.Filter.ElemMatch(x => x.Users, y => y.Id == userId);
            var update = Builders<Notification>.Update.Set("Users.$.IsRead", true);

            var updateResult = await _notifications.UpdateManyAsync(filter, update);

            return updateResult.ModifiedCount > 0;
        }

        public void Remove(string id)
        {
            _notifications.DeleteOne(notification => notification.Id == id);
        }
        public void Update(string id, Notification notification)
        {
            _notifications.ReplaceOne(notification=> notification.Id == id,notification);
        }
    }
}
