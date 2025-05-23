using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectController : Controller
    {
        private readonly IUserProjectService _service;

        public UserProjectController(IUserProjectService service)
        {
            _service = service;
        }

        // GET: api/UserProject/GetUserProjects
        [HttpGet]
        [Route("GetUserProjects")]
        public IActionResult GetUserProjects()
        {
            var userProjects = _service.GetUserProjects();
            if (userProjects != null && userProjects.Any())
            {
                return Ok(userProjects);
            }
            return NotFound("No user project assignments found.");
        }

        // GET: api/UserProject/GetUserProjectById/{id}
        [HttpGet]
        [Route("GetUserProjectById/{id}")]
        public IActionResult GetUserProjectById(int id)
        {
            var userProject = _service.GetUserProjectById(id);
            if (userProject != null)
            {
                return Ok(userProject);
            }
            return NotFound($"User project assignment with ID {id} not found.");
        }

        // GET: api/UserProject/GetProjectMembers/{projectId}
        [HttpGet]
        [Route("GetProjectMembers/{projectId}")]
        public IActionResult GetProjectMembers(int projectId)
        {
            var members = _service.GetProjectMembers(projectId);
            if (members != null && members.Any())
            {
                return Ok(members);
            }
            return NotFound($"No members found for project with ID {projectId}.");
        }

        // GET: api/UserProject/GetUserAssignments/{userId}
        [HttpGet]
        [Route("GetUserAssignments/{userId}")]
        public IActionResult GetUserAssignments(int userId)
        {
            var assignments = _service.GetUserAssignments(userId);
            if (assignments != null && assignments.Any())
            {
                return Ok(assignments);
            }
            return NotFound($"No project assignments found for user with ID {userId}.");
        }

        // POST: api/UserProject/AddUserProject
        [HttpPost]
        [Route("AddUserProject")]
        public IActionResult AddUserProject([FromBody] UserProject userProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.AddUserProject(userProject);
            return CreatedAtAction(nameof(GetUserProjectById), new { id = userProject.UserProjectID }, userProject);
        }

        // PUT: api/UserProject/UpdateUserProject/{id}
        [HttpPut]
        [Route("UpdateUserProject/{id}")]
        public IActionResult UpdateUserProject(int id, [FromBody] UserProject userProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUserProject = _service.GetUserProjectById(id);
            if (existingUserProject == null)
            {
                return NotFound($"User project assignment with ID {id} not found.");
            }

            userProject.UserProjectID = id;
            _service.UpdateUserProject(userProject);

            return NoContent();
        }

        // DELETE: api/UserProject/DeleteUserProject/{id}
        [HttpDelete]
        [Route("DeleteUserProject/{id}")]
        public IActionResult DeleteUserProject(int id)
        {
            var userProject = _service.GetUserProjectById(id);
            if (userProject == null)
            {
                return NotFound($"User project assignment with ID {id} not found.");
            }

            _service.DeleteUserProject(id);
            return NoContent();
        }
    }
}
