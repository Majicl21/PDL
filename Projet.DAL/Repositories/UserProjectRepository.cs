using Project.Context;
using Project.Entities;

namespace Project.DAL.Repositories
{
    public class UserProjectRepository : GenericRepository<UserProject>
    {
        public UserProjectRepository(DataContext context) : base(context)
        {
        }
    }
}
