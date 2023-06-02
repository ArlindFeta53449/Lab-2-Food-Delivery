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
        IList<Notification> GetNotifications();
        Notification GetNotificationById(string id);
        void Remove(string id);
        void Update(string id, Notification notification);
    }
}
