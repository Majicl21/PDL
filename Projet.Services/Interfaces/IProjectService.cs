using Project.Entities;

namespace Project.Services.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<Project.Entities.Project> GetProjects();
        Project.Entities.Project GetProjectById(int id);
        void AddProject(Project.Entities.Project project);
        void UpdateProject(Project.Entities.Project project);
        void DeleteProject(int id);
        IEnumerable<Project.Entities.Project> SearchProjects(string searchTerm);
    }
}
