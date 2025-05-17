using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public Utilisateur User { get; set; }
    }
}
