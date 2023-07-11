using Business.Services.NotificationService;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NotificationMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        // GET: api/<NotificationsController>
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService) {
            _notificationService = notificationService;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetNotifications(string id)
        {
            var response = _notificationService.GetNotifications(id);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public IActionResult GetNotificationById(string id,string userId)
        {
            var notification = _notificationService.GetNotificationById(id,userId);

            if(notification == null)
            {
                return NotFound("Notification not found");
            }
            return Ok(notification);
        }
        [HttpPut("{notificationId}/{userId}")]
        public async Task<IActionResult> MarkNotificationAsRead(string notificationId,string userId) {
            var response = await _notificationService.MarkNotificationAsRead(notificationId, userId);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> MarkAllNotificationsAsRead(string userId)
        {
            var response = await _notificationService.MarkAllNotificationAsRead(userId);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> ClearAllNotificationsForUser(string userId)
        {
            var response =await _notificationService.ClearAllNotificationsForUser(userId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(string id,string userId)
        {
            var notification = _notificationService.GetNotificationById(id, userId);
            if(notification == null)
            {
                return NotFound("The notification was not found");
            }
            _notificationService.DeleteNotification(id);
            return Ok("The notification was deleted successfully.");
        }
    }
}
