using Microsoft.EntityFrameworkCore;
using Project.Entities;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Project.Context
{
    public class DataContext : DbContext
    {        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
        public virtual DbSet<Project.Entities.Project> Projects { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Timesheet> Timesheets { get; set; }
        public virtual DbSet<UserProject> UserProjects { get; set; }        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Utilisateur-Project many-to-many relationship through UserProject
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(up => up.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(up => up.ProjectID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Utilisateur-Timesheet one-to-many relationship
            modelBuilder.Entity<Timesheet>()
                .HasOne(t => t.User)
                .WithMany(u => u.Timesheets)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Project-Timesheet one-to-many relationship
            modelBuilder.Entity<Timesheet>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Timesheets)
                .HasForeignKey(t => t.ProjectID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Utilisateur-Notification one-to-many relationship
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Project creator relationship
            modelBuilder.Entity<Project.Entities.Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.CreatedProjects)
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Add indexes for better query performance
            modelBuilder.Entity<Utilisateur>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Project.Entities.Project>()
                .HasIndex(p => p.ProjectName);

            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.UserID);

            modelBuilder.Entity<Timesheet>()
                .HasIndex(t => new { t.UserID, t.ProjectID, t.Date });
        }
    }
}