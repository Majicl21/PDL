using Project.BLL.Contracts;
using Project.Entities;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IGenericBLL<Notification> _notificationBLL;

        public NotificationService(IGenericBLL<Notification> notificationBLL)
        {
            _notificationBLL = notificationBLL;
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _notificationBLL.GetMany();
        }

        public Notification GetNotificationById(int id)
        {
            return _notificationBLL.GetById(id);
        }

        public void AddNotification(Notification notification)
        {
            notification.CreatedAt = DateTime.UtcNow;
            notification.IsRead = false;
            _notificationBLL.Add(notification);
            _notificationBLL.Submit();
        }

        public void UpdateNotification(Notification notification)
        {
            _notificationBLL.Update(notification);
            _notificationBLL.Submit();
        }

        public void DeleteNotification(int id)
        {
            var notification = _notificationBLL.GetById(id);
            if (notification != null)
            {
                _notificationBLL.Delete(notification);
                _notificationBLL.Submit();
            }
        }

        public IEnumerable<Notification> GetUserNotifications(int userId)
        {
            return _notificationBLL.GetMany(n => n.UserID == userId);
        }

        public void MarkNotificationAsRead(int id)
        {
            var notification = _notificationBLL.GetById(id);
            if (notification != null)
            {
                notification.IsRead = true;
                _notificationBLL.Update(notification);
                _notificationBLL.Submit();
            }
        }
    }
}
