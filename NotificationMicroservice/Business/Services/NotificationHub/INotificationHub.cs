using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.NotificationHub
{
    public interface INotificationHub
    {
        Task SendNotification(Notification notification);
    }
}
