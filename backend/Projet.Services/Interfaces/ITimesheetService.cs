using Project.Entities;

namespace Project.Services.Interfaces
{
    public interface ITimesheetService
    {
        IEnumerable<Timesheet> GetTimesheets();
        Timesheet GetTimesheetById(int id);
        void AddTimesheet(Timesheet timesheet);
        void UpdateTimesheet(Timesheet timesheet);
        void DeleteTimesheet(int id);
        IEnumerable<Timesheet> GetUserTimesheets(int userId);
        IEnumerable<Timesheet> GetProjectTimesheets(int projectId);
        void ApproveTimesheet(int id);
    }
}
