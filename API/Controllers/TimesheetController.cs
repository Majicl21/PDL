using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : Controller
    {
        private readonly ITimesheetService _service;

        public TimesheetController(ITimesheetService service)
        {
            _service = service;
        }

        // GET: api/Timesheet/GetTimesheets
        [HttpGet]
        [Route("GetTimesheets")]
        public IActionResult GetTimesheets()
        {
            var timesheets = _service.GetTimesheets();
            if (timesheets != null && timesheets.Any())
            {
                return Ok(timesheets);
            }
            return NotFound("No timesheets found.");
        }

        // GET: api/Timesheet/GetTimesheetById/{id}
        [HttpGet]
        [Route("GetTimesheetById/{id}")]
        public IActionResult GetTimesheetById(int id)
        {
            var timesheet = _service.GetTimesheetById(id);
            if (timesheet != null)
            {
                return Ok(timesheet);
            }
            return NotFound($"Timesheet with ID {id} not found.");
        }

        // GET: api/Timesheet/GetUserTimesheets/{userId}
        [HttpGet]
        [Route("GetUserTimesheets/{userId}")]
        public IActionResult GetUserTimesheets(int userId)
        {
            var timesheets = _service.GetUserTimesheets(userId);
            if (timesheets != null && timesheets.Any())
            {
                return Ok(timesheets);
            }
            return NotFound($"No timesheets found for user with ID {userId}.");
        }

        // GET: api/Timesheet/GetProjectTimesheets/{projectId}
        [HttpGet]
        [Route("GetProjectTimesheets/{projectId}")]
        public IActionResult GetProjectTimesheets(int projectId)
        {
            var timesheets = _service.GetProjectTimesheets(projectId);
            if (timesheets != null && timesheets.Any())
            {
                return Ok(timesheets);
            }
            return NotFound($"No timesheets found for project with ID {projectId}.");
        }

        // POST: api/Timesheet/AddTimesheet
        [HttpPost]
        [Route("AddTimesheet")]
        public IActionResult AddTimesheet([FromBody] Timesheet timesheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.AddTimesheet(timesheet);
            return CreatedAtAction(nameof(GetTimesheetById), new { id = timesheet.TimesheetID }, timesheet);
        }

        // PUT: api/Timesheet/UpdateTimesheet/{id}
        [HttpPut]
        [Route("UpdateTimesheet/{id}")]
        public IActionResult UpdateTimesheet(int id, [FromBody] Timesheet timesheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTimesheet = _service.GetTimesheetById(id);
            if (existingTimesheet == null)
            {
                return NotFound($"Timesheet with ID {id} not found.");
            }

            timesheet.TimesheetID = id;
            _service.UpdateTimesheet(timesheet);

            return NoContent();
        }

        // PUT: api/Timesheet/ApproveTimesheet/{id}
        [HttpPut]
        [Route("ApproveTimesheet/{id}")]
        public IActionResult ApproveTimesheet(int id)
        {
            var timesheet = _service.GetTimesheetById(id);
            if (timesheet == null)
            {
                return NotFound($"Timesheet with ID {id} not found.");
            }

            _service.ApproveTimesheet(id);
            return NoContent();
        }

        // DELETE: api/Timesheet/DeleteTimesheet/{id}
        [HttpDelete]
        [Route("DeleteTimesheet/{id}")]
        public IActionResult DeleteTimesheet(int id)
        {
            var timesheet = _service.GetTimesheetById(id);
            if (timesheet == null)
            {
                return NotFound($"Timesheet with ID {id} not found.");
            }

            _service.DeleteTimesheet(id);
            return NoContent();
        }
    }
}
