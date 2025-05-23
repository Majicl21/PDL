using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class UserProject
    {
        [Key]
        public int UserProjectID { get; set; }

        public int UserID { get; set; }
        public int ProjectID { get; set; }

        public DateTime AssignedAt { get; set; }

        public Utilisateur User { get; set; }
        public Project Project { get; set; }
    }
}
