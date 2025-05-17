using Microsoft.EntityFrameworkCore;
using Project.Entities;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Project.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*// Utilisateur - Projet (One-to-Many)
            modelBuilder.Entity<Projet>()
                .HasOne(p => p.ChefDeProjet)
                .WithMany(u => u.Projets)
                .HasForeignKey(p => p.IdChefDeProjet);

            // Projet - Sprint (One-to-Many)
            modelBuilder.Entity<Sprint>()
                .HasOne(s => s.Projet)
                .WithMany(p => p.Sprints)
                .HasForeignKey(s => s.IdProjet);

            // Sprint - Tache (One-to-Many)
            modelBuilder.Entity<Tache>()
                .HasOne(t => t.Sprint)
                .WithMany(s => s.Taches)
                .HasForeignKey(t => t.IdSprint);

            // Tache - Utilisateur (One-to-Many for employee assignment)
            modelBuilder.Entity<Tache>()
                .HasOne(t => t.Employe)
                .WithMany(u => u.Taches)
                .HasForeignKey(t => t.IdEmploye);*/
        }
    }
}