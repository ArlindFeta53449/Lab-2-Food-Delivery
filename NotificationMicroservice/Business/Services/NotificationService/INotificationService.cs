using Data.DTOs;
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
        ApiResponse<IList<NotificationForDisplayDto>> GetNotifications(string userId);
        Task<ApiResponse<string>> ClearAllNotificationsForUser(string userId);
        Task<ApiResponse<string>> MarkNotificationAsRead(string notificationId, string userId);
        Task<ApiResponse<string>> MarkAllNotificationAsRead(string userId);
        Notification GetNotificationById(string id, string userId);
        void UpdateNotification(string id, Notification notification);
        void DeleteNotification(string id);
    }
}
