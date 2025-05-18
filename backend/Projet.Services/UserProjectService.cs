using Project.BLL.Contracts;
using Project.Entities;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class UserProjectService : IUserProjectService
    {
        private readonly IGenericBLL<UserProject> _userProjectBLL;

        public UserProjectService(IGenericBLL<UserProject> userProjectBLL)
        {
            _userProjectBLL = userProjectBLL;
        }

        public IEnumerable<UserProject> GetUserProjects()
        {
            return _userProjectBLL.GetMany();
        }

        public UserProject GetUserProjectById(int id)
        {
            return _userProjectBLL.GetById(id);
        }

        public void AddUserProject(UserProject userProject)
        {
            userProject.AssignedAt = DateTime.UtcNow;
            _userProjectBLL.Add(userProject);
            _userProjectBLL.Submit();
        }

        public void UpdateUserProject(UserProject userProject)
        {
            _userProjectBLL.Update(userProject);
            _userProjectBLL.Submit();
        }

        public void DeleteUserProject(int id)
        {
            var userProject = _userProjectBLL.GetById(id);
            if (userProject != null)
            {
                _userProjectBLL.Delete(userProject);
                _userProjectBLL.Submit();
            }
        }

        public IEnumerable<UserProject> GetProjectMembers(int projectId)
        {
            return _userProjectBLL.GetMany(up => up.ProjectID == projectId);
        }

        public IEnumerable<UserProject> GetUserAssignments(int userId)
        {
            return _userProjectBLL.GetMany(up => up.UserID == userId);
        }
    }
}
