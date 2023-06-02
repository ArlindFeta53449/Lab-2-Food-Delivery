using Data.Entities;
using Data.Interfaces;
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
        public IList<Notification> GetNotifications()
        {
            return _notifications.Find(notification => true).ToList();
        }
        public Notification GetNotificationById(string id)
        {
            return _notifications.Find(notification => notification.Id == id).FirstOrDefault();
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
