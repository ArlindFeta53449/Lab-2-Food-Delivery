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
        
        [HttpGet]
        public IActionResult GetNotifications(string userId)
        {
            return StatusCode(200, _notificationService.GetNotifications(userId));
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
        [HttpPost]
        public IActionResult CreateNotification([FromBody]Notification notification)
        {
            _notificationService.CreateNotification(notification);
            return Ok(notification);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateNotification(string id,string userId,[FromBody]Notification notification) {
            var notificationInDb = _notificationService.GetNotificationById(id,userId);

            if(notificationInDb == null)
            {
                return NotFound("The notification was not found");
            }
            _notificationService.UpdateNotification(id,notification);
            return Ok();
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
