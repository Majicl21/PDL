using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Services.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : Controller
    {
        private readonly IUtilisateurService _service;

        public UtilisateurController(IUtilisateurService service)
        {
            _service = service;
        }

        // GET: api/User/GetUsers
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            var listUsers = _service.GetUsers();
            if (listUsers != null && listUsers.Any())
            {
                return Ok(listUsers);
            }
            return NotFound("No users found.");
        }

        // GET: api/User/GetUserById/{id}
        [HttpGet]
        [Route("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _service.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound($"User with ID {id} not found.");
        }

        // POST: api/User/AddUser
        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.AddUser(utilisateur);
            return CreatedAtAction(nameof(GetUserById), new { id = utilisateur.Id }, utilisateur);
        }

        // PUT: api/User/UpdateUser/{id}
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = _service.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            utilisateur.Id = id; // Ensure the ID matches
            _service.UpdateUser(utilisateur);

            return NoContent();
        }

        // DELETE: api/User/DeleteUser/{id}
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _service.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            _service.DeleteUser(id);
            return NoContent();
        }

        // GET: api/User/SearchUsers?term=example
        [HttpGet]
        [Route("SearchUsers")]
        public IActionResult SearchUsers([FromQuery] string term)
        {
            var users = _service.SearchUsers(term);
            if (users != null && users.Any())
            {
                return Ok(users);
            }
            return NotFound($"No users found matching the term '{term}'.");
        }
    }
}
