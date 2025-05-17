using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Project.Entities;

namespace Project.Services.Interfaces
{
   
    public interface IAuthService
    {
        string GenerateToken(Utilisateur user);
        Utilisateur Authenticate(string username, string password);
    }
}
