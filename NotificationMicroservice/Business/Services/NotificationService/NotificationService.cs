using Data.Entities;
using Repository.NotificationsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.NotificationService
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository) {
            _notificationRepository = notificationRepository;
        }

        public IList<Notification> GetNotifications(string userId)
        {
            return _notificationRepository.GetNotifications(userId);
        }
        public Notification GetNotificationById(string id,string userId)
        {
            return _notificationRepository.GetNotificationById(id,userId);
        }
        public Notification CreateNotification(Notification notification)
        {
            return _notificationRepository.Create(notification);
        }
        public void UpdateNotification(string id,Notification notification) {
             _notificationRepository.Update(id, notification);
        }
        public void DeleteNotification(string id)
        {
            _notificationRepository.Remove(id);
        }
    }
}
