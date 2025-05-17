using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        public string ProjectName { get; set; }
        public string Description { get; set; }

        [ForeignKey("User")]
        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public Utilisateur User { get; set; }
    }
}
