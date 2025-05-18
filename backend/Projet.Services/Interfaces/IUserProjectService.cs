using Project.Entities;

namespace Project.Services.Interfaces
{
    public interface IUserProjectService
    {
        IEnumerable<UserProject> GetUserProjects();
        UserProject GetUserProjectById(int id);
        void AddUserProject(UserProject userProject);
        void UpdateUserProject(UserProject userProject);
        void DeleteUserProject(int id);
        IEnumerable<UserProject> GetProjectMembers(int projectId);
        IEnumerable<UserProject> GetUserAssignments(int userId);
    }
}
