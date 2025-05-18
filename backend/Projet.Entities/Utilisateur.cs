using System.ComponentModel.DataAnnotations;
using Project.Enums;
using System.Collections.Generic;

namespace Project.Entities
{
    public class Utilisateur
    {
        public Utilisateur()
        {
            UserProjects = new List<UserProject>();
            Timesheets = new List<Timesheet>();
            Notifications = new List<Notification>();
            CreatedProjects = new List<Project>();
        }

        [Key]  // Marks the property as the primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre 2 et 50 caractères.")]
        public string? Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom doit contenir entre 2 et 50 caractères.")]
        public string? Prenom { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        [StringLength(100, ErrorMessage = "L'email ne peut pas dépasser 100 caractères.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
        public string? MotDePasse { get; set; }

        [Required(ErrorMessage = "Le rôle est obligatoire.")]
        public Role Role { get; set; }

        // Navigation properties
        public virtual ICollection<UserProject> UserProjects { get; set; }
        public virtual ICollection<Timesheet> Timesheets { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Project> CreatedProjects { get; set; }
    }
}

