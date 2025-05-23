using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;


namespace Project.Services.Interfaces
{
    public interface IUtilisateurService
    {
        IEnumerable<Utilisateur> GetUsers();

        Utilisateur GetUserById(int id);

        void AddUser(Utilisateur user);

        void UpdateUser(Utilisateur user);

        void DeleteUser(int id);

        IEnumerable<Utilisateur> SearchUsers(string searchTerm);



    }
}
