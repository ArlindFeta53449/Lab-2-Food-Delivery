using Data.DTOs;
using Data.Entities;
using Repository.NotificationsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository) {
            _notificationRepository = notificationRepository;
        }
        public async Task<ApiResponse<string>> ClearAllNotificationsForUser(string userId)
        {
            try
            {
                var result = await _notificationRepository.ClearAllNotificationsForUser(userId);
                if (result)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The notifications were cleared"
                    };
                }
                return new ApiResponse<string>() {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "There were no notifications to clear" }
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public async Task<ApiResponse<string>> MarkNotificationAsRead(string notificationId, string userId)
        {
            try
            {
                var result = await _notificationRepository.MarkNotificationAsRead(notificationId, userId);
                if (result)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "There was a problem with marking the notification as read"}
                };
            }catch(Exception ex)
            {
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public async Task<ApiResponse<string>> MarkAllNotificationAsRead(string userId)
        {
            try
            {
                var result = await _notificationRepository.MarkAllNotificationsAsRead(userId);
                if (result)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "There was a problem with marking the notifications as read" }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<IList<NotificationForDisplayDto>> GetNotifications(string userId)
        {
            try
            {
                var notifications = _notificationRepository.GetNotifications(userId);
                return new ApiResponse<IList<NotificationForDisplayDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = notifications
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<NotificationForDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public Notification GetNotificationById(string id,string userId)
        {
            return _notificationRepository.GetNotificationById(id,userId);
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
