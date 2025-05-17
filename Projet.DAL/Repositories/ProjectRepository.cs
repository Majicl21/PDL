using Project.Context;
using Project.Entities;

namespace Project.DAL.Repositories
{
    public class ProjectRepository : GenericRepository<Project.Entities.Project>
    {
        public ProjectRepository(DataContext context) : base(context)
        {
        }
    }
}
