using Project.DAL.Contracts;
using Project.Entities;

namespace Project.DAL
{    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, IRepository> _repositories;

        public UnitOfWork(
            IRepository<Utilisateur> utilisateurRepo,
            IRepository<Project.Entities.Project> projectRepo,
            IRepository<Notification> notificationRepo,
            IRepository<Timesheet> timesheetRepo,
            IRepository<UserProject> userProjectRepo)
        {
            _repositories = _repositories ?? new Dictionary<Type, IRepository>();
            _repositories.Add(typeof(Utilisateur), utilisateurRepo);
            _repositories.Add(typeof(Project.Entities.Project), projectRepo);
            _repositories.Add(typeof(Notification), notificationRepo);
            _repositories.Add(typeof(Timesheet), timesheetRepo);
            _repositories.Add(typeof(UserProject), userProjectRepo);
        }

        public IRepository GetRepository<T>() where T : class
        {
            return _repositories[typeof(T)];
        }
    }
}