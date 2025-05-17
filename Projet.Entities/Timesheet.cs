using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class Timesheet
    {
        [Key]
        public int TimesheetID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }
        public string Comment { get; set; }
        public bool ApprovedByManager { get; set; }

        public Utilisateur User { get; set; }
        public Project Project { get; set; }
    }
}
