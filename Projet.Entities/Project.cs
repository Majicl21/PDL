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
        public Project()
        {
            UserProjects = new List<UserProject>();
            Timesheets = new List<Timesheet>();
        }

        [Key]
        public int ProjectID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        [ForeignKey("User")]
        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual Utilisateur User { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
