using Microsoft.AspNetCore.Http;
using Project.BLL.Contracts;
using Project.Entities;
using Project.Services.Interfaces;
using System.Security.Claims;

namespace Project.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly IGenericBLL<Timesheet> _timesheetBLL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenericBLL<Utilisateur> _userBLL;

        public TimesheetService(
            IGenericBLL<Timesheet> timesheetBLL,
            IHttpContextAccessor httpContextAccessor,
            IGenericBLL<Utilisateur> userBLL)
        {
            _timesheetBLL = timesheetBLL;
            _httpContextAccessor = httpContextAccessor;
            _userBLL = userBLL;
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

        public async Task<Utilisateur> GetCurrentUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            return await Task.FromResult(_userBLL.GetById(int.Parse(userId)));
        }

        public async Task<IEnumerable<Timesheet>> GetMyTimesheets()
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null)
                return Enumerable.Empty<Timesheet>();

            return _timesheetBLL.GetMany(t => t.UserID == currentUser.Id);
        }

        public async Task<IEnumerable<Timesheet>> FindMyTimesheets(DateTime startDate, DateTime endDate)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null)
                return Enumerable.Empty<Timesheet>();

            return _timesheetBLL.GetMany(t =>
                t.UserID == currentUser.Id &&
                t.Date >= startDate.Date &&
                t.Date <= endDate.Date);
        }

        public async Task<Timesheet> GetTimesheet(int id)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null)
                return null;

            return _timesheetBLL.GetMany(t =>
                t.TimesheetID == id &&
                t.UserID == currentUser.Id).FirstOrDefault();
        }
    }
}
