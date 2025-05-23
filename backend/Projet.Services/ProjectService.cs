using Project.BLL.Contracts;
using Project.Entities;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class ProjectService : IProjectService
    {
        private IGenericBLL<Project.Entities.Project> _projectBLL;

        public ProjectService(IGenericBLL<Project.Entities.Project> projectBLL)
        {
            _projectBLL = projectBLL;
        }

        public IEnumerable<Project.Entities.Project> GetProjects()
        {
            return _projectBLL.GetMany();
        }

        public Project.Entities.Project GetProjectById(int id)
        {
            return _projectBLL.GetById(id);
        }

        public void AddProject(Project.Entities.Project project)
        {
            project.CreatedAt = DateTime.UtcNow;
            _projectBLL.Add(project);
            _projectBLL.Submit();
        }

        public void UpdateProject(Project.Entities.Project project)
        {
            _projectBLL.Update(project);
            _projectBLL.Submit();
        }

        public void DeleteProject(int id)
        {
            var project = _projectBLL.GetById(id);
            if (project != null)
            {
                _projectBLL.Delete(project);
                _projectBLL.Submit();
            }
        }

        public IEnumerable<Project.Entities.Project> SearchProjects(string searchTerm)
        {
            return _projectBLL.GetMany(p => 
                p.ProjectName.Contains(searchTerm) || 
                p.Description.Contains(searchTerm)
            );
        }
    }
}
