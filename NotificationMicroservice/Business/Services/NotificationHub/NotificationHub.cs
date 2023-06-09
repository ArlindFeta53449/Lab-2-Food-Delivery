using Data.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.NotificationHub
{
    public class NotificationHub : Hub,INotificationHub
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotification(Notification notification)
        {
            try
            {
                if (notification == null)
                {
                    Console.WriteLine("Notification object is null");
                    throw new ArgumentNullException(nameof(notification));
                }

                await _hubContext.Clients.All.SendAsync("NewNotification", notification);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending notification: {ex}");
                throw; // Optionally rethrow the exception
            }
        }
    }

}
