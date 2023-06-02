using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.NotificationService
{
    public interface INotificationService
    {
        IList<Notification> GetNotifications();
        Notification GetNotificationById(string id);
        Notification CreateNotification(Notification notification);
        void UpdateNotification(string id, Notification notification);
        void DeleteNotification(string id);
    }
}
