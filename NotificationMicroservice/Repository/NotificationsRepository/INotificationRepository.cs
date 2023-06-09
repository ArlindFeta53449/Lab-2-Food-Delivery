using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.NotificationsRepository
{
    public interface INotificationRepository
    {
        Notification Create(Notification notification);
        IList<NotificationForDisplayDto> GetNotifications(string userId);
        Task<bool> ClearAllNotificationsForUser(string userId);
        Notification GetNotificationById(string id, string userId);
        void Remove(string id);
        void Update(string id, Notification notification);
        Task<bool> MarkNotificationAsRead(string notificationId, string userId);
        Task<bool> MarkAllNotificationsAsRead(string userId);
    }
}
