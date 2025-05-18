using Project.Context;
using Project.Entities;

namespace Project.DAL.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>
    {
        public NotificationRepository(DataContext context) : base(context)
        {
        }
    }
}
