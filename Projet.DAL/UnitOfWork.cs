using Project.DAL.Contracts;
using Project.Entities;

namespace Project.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, IRepository> _repositories;

        public UnitOfWork(IRepository<Utilisateur> utilisateurRepo)
        {
            _repositories = _repositories ?? new Dictionary<Type, IRepository>();
            _repositories.Add(typeof(Utilisateur), utilisateurRepo);


        }
        public IRepository GetRepository<T>() where T : class
        {
            return _repositories[typeof(T)];
        }
    }
}