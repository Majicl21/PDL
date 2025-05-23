using Project.BLL.Contracts;
using Project.Entities;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private IGenericBLL<Utilisateur> _userBLL;
        public UtilisateurService(IGenericBLL<Utilisateur> userBll)
        {
            _userBLL = userBll;
        }
        public IEnumerable<Utilisateur> GetUsers()
        {
            return _userBLL.GetMany();

        }
        public Utilisateur GetUserById(int id)
        {
            return _userBLL.GetById(id);
        }

        public void AddUser(Utilisateur utilisateur)
        {
            _userBLL.Add(utilisateur);
            _userBLL.Submit();
        }

        public void UpdateUser(Utilisateur utilisateur)
        {
            _userBLL.Update(utilisateur);
            _userBLL.Submit();
        }

        public void DeleteUser(int id)
        {
            var utilisateur = _userBLL.GetById(id);
            if (utilisateur != null)
            {
                _userBLL.Delete(utilisateur);
                _userBLL.Submit();
            }
        }

        public IEnumerable<Utilisateur> SearchUsers(string searchTerm)
        {
            return _userBLL.GetMany(
                u => u.Nom.Contains(searchTerm) || u.Prenom.Contains(searchTerm) || u.Email.Contains(searchTerm)
            );
        }
    }
}