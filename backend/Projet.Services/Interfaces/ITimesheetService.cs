using Project.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<Utilisateur> GetCurrentUser();
        Task<IEnumerable<Timesheet>> GetMyTimesheets();
        Task<IEnumerable<Timesheet>> FindMyTimesheets(DateTime startDate, DateTime endDate);
        Task<Timesheet> GetTimesheet(int id);
    }
}
