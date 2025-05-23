using Project.Entities;

namespace Project.Services.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetNotifications();
        Notification GetNotificationById(int id);
        void AddNotification(Notification notification);
        void UpdateNotification(Notification notification);
        void DeleteNotification(int id);
        IEnumerable<Notification> GetUserNotifications(int userId);
        void MarkNotificationAsRead(int id);
    }
}
