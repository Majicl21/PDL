using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        // GET: api/Project/GetProjects
        [HttpGet]
        [Route("GetProjects")]
        public IActionResult GetProjects()
        {
            var projects = _service.GetProjects();
            if (projects != null && projects.Any())
            {
                return Ok(projects);
            }
            return NotFound("No projects found.");
        }

        // GET: api/Project/GetProjectById/{id}
        [HttpGet]
        [Route("GetProjectById/{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _service.GetProjectById(id);
            if (project != null)
            {
                return Ok(project);
            }
            return NotFound($"Project with ID {id} not found.");
        }

        // POST: api/Project/AddProject
        [HttpPost]
        [Route("AddProject")]
        public IActionResult AddProject([FromBody] Project.Entities.Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.AddProject(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectID }, project);
        }

        // PUT: api/Project/UpdateProject/{id}
        [HttpPut]
        [Route("UpdateProject/{id}")]
        public IActionResult UpdateProject(int id, [FromBody] Project.Entities.Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProject = _service.GetProjectById(id);
            if (existingProject == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            project.ProjectID = id;
            _service.UpdateProject(project);

            return NoContent();
        }

        // DELETE: api/Project/DeleteProject/{id}
        [HttpDelete]
        [Route("DeleteProject/{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _service.GetProjectById(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            _service.DeleteProject(id);
            return NoContent();
        }

        // GET: api/Project/SearchProjects?term=example
        [HttpGet]
        [Route("SearchProjects")]
        public IActionResult SearchProjects([FromQuery] string term)
        {
            var projects = _service.SearchProjects(term);
            if (projects != null && projects.Any())
            {
                return Ok(projects);
            }
            return NotFound($"No projects found matching the term '{term}'.");
        }
    }
}
