using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        // GET: api/Notification/GetNotifications
        [HttpGet]
        [Route("GetNotifications")]
        public IActionResult GetNotifications()
        {
            var notifications = _service.GetNotifications();
            if (notifications != null && notifications.Any())
            {
                return Ok(notifications);
            }
            return NotFound("No notifications found.");
        }

        // GET: api/Notification/GetNotificationById/{id}
        [HttpGet]
        [Route("GetNotificationById/{id}")]
        public IActionResult GetNotificationById(int id)
        {
            var notification = _service.GetNotificationById(id);
            if (notification != null)
            {
                return Ok(notification);
            }
            return NotFound($"Notification with ID {id} not found.");
        }

        // GET: api/Notification/GetUserNotifications/{userId}
        [HttpGet]
        [Route("GetUserNotifications/{userId}")]
        public IActionResult GetUserNotifications(int userId)
        {
            var notifications = _service.GetUserNotifications(userId);
            if (notifications != null && notifications.Any())
            {
                return Ok(notifications);
            }
            return NotFound($"No notifications found for user with ID {userId}.");
        }

        // POST: api/Notification/AddNotification
        [HttpPost]
        [Route("AddNotification")]
        public IActionResult AddNotification([FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.AddNotification(notification);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.NotificationID }, notification);
        }

        // PUT: api/Notification/UpdateNotification/{id}
        [HttpPut]
        [Route("UpdateNotification/{id}")]
        public IActionResult UpdateNotification(int id, [FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingNotification = _service.GetNotificationById(id);
            if (existingNotification == null)
            {
                return NotFound($"Notification with ID {id} not found.");
            }

            notification.NotificationID = id;
            _service.UpdateNotification(notification);

            return NoContent();
        }

        // PUT: api/Notification/MarkAsRead/{id}
        [HttpPut]
        [Route("MarkAsRead/{id}")]
        public IActionResult MarkAsRead(int id)
        {
            var notification = _service.GetNotificationById(id);
            if (notification == null)
            {
                return NotFound($"Notification with ID {id} not found.");
            }

            _service.MarkNotificationAsRead(id);
            return NoContent();
        }

        // DELETE: api/Notification/DeleteNotification/{id}
        [HttpDelete]
        [Route("DeleteNotification/{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _service.GetNotificationById(id);
            if (notification == null)
            {
                return NotFound($"Notification with ID {id} not found.");
            }

            _service.DeleteNotification(id);
            return NoContent();
        }
    }
}
