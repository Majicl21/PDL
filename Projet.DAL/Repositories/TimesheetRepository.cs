using Project.Context;
using Project.Entities;

namespace Project.DAL.Repositories
{
    public class TimesheetRepository : GenericRepository<Timesheet>
    {
        public TimesheetRepository(DataContext context) : base(context)
        {
        }
    }
}
