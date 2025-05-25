using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Project.Entities;
using Project.Services.Interfaces;
using System.Diagnostics;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : Controller
    {
        private readonly IUtilisateurService _service;
        private readonly ILogger<UtilisateurController> _logger;

        public UtilisateurController(IUtilisateurService service, ILogger<UtilisateurController> logger)
        {
            _service = service;
            _logger = logger;
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

            return Ok(utilisateur);
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

        // GET: api/Utilisateur/current-user
        [HttpGet]
        [Authorize]
        [Route("current-user")]
        public IActionResult GetCurrentUser()
        {
            _logger.LogInformation("GetCurrentUser endpoint called.");

            // Follow the logic from the logout function: get email from claims
            var emailClaim = User.FindFirst(ClaimTypes.Email) ?? User.FindFirst("email");
            if (emailClaim == null)
            {
                _logger.LogWarning("Email not found in token claims.");
                return Unauthorized("Email not found in token.");
            }

            var email = emailClaim.Value;
            _logger.LogInformation("Extracted email from token: {Email}", email);

            // Find user by email (since your JWT does not contain user ID, but does contain email)
            var user = _service.GetUsers().FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found.", email);
                return NotFound($"User with email {email} not found.");
            }

            _logger.LogInformation("Returning user: {Prenom} {Nom} ({Email})", user.Prenom, user.Nom, user.Email);
            return Ok(user);
        }
    }
}
