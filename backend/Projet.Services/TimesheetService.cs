using Project.BLL.Contracts;
using Project.Entities;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly IGenericBLL<Timesheet> _timesheetBLL;

        public TimesheetService(IGenericBLL<Timesheet> timesheetBLL)
        {
            _timesheetBLL = timesheetBLL;
        }

        public IEnumerable<Timesheet> GetTimesheets()
        {
            return _timesheetBLL.GetMany();
        }

        public Timesheet GetTimesheetById(int id)
        {
            return _timesheetBLL.GetById(id);
        }

        public void AddTimesheet(Timesheet timesheet)
        {
            timesheet.Date = DateTime.UtcNow.Date;
            timesheet.ApprovedByManager = false;
            _timesheetBLL.Add(timesheet);
            _timesheetBLL.Submit();
        }

        public void UpdateTimesheet(Timesheet timesheet)
        {
            _timesheetBLL.Update(timesheet);
            _timesheetBLL.Submit();
        }

        public void DeleteTimesheet(int id)
        {
            var timesheet = _timesheetBLL.GetById(id);
            if (timesheet != null)
            {
                _timesheetBLL.Delete(timesheet);
                _timesheetBLL.Submit();
            }
        }

        public IEnumerable<Timesheet> GetUserTimesheets(int userId)
        {
            return _timesheetBLL.GetMany(t => t.UserID == userId);
        }

        public IEnumerable<Timesheet> GetProjectTimesheets(int projectId)
        {
            return _timesheetBLL.GetMany(t => t.ProjectID == projectId);
        }

        public void ApproveTimesheet(int id)
        {
            var timesheet = _timesheetBLL.GetById(id);
            if (timesheet != null)
            {
                timesheet.ApprovedByManager = true;
                _timesheetBLL.Update(timesheet);
                _timesheetBLL.Submit();
            }
        }
    }
}
