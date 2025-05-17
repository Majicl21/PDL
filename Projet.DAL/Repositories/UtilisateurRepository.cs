using Project.Context;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories
{
    public class UtilisateurRepository : GenericRepository<Utilisateur>
    {
        public UtilisateurRepository(DataContext context) : base(context)
        {
        }
    }
}
