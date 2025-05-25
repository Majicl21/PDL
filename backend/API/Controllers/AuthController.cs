using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Project.Entities;
    using Project.Services;
    using Project.Services.Interfaces;

    namespace API.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IAuthService _authService;
            private readonly IUtilisateurService _utilisateurService;


            public AuthController(IAuthService authService,IUtilisateurService utilisateurService)
            {
                _authService = authService;
                _utilisateurService = utilisateurService;
            }

            [HttpPost("register")]
            public IActionResult Register([FromBody] Utilisateur user)
            {
                user.MotDePasse = BCrypt.Net.BCrypt.HashPassword(user.MotDePasse);
                try
                {
                    _utilisateurService.AddUser(user);
                    return Ok("User registered successfully!");
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Utilisateurs_Email"))
                    {
                        return BadRequest("This email is already registered. Please use another email.");
                    }
                    return StatusCode(500, "An error occurred while registering the user.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginRequest model)
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                    return BadRequest("Email and password are required.");

                var user = _authService.Authenticate(model.Email, model.Password);

                if (user == null)
                    return Unauthorized("Invalid credentials");

                var token = _authService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            [HttpPost("logout")]
            [Authorize]
            public IActionResult Logout()
            {
                // Retrieve the token from the Authorization header
                var authorizationHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                {
                    return Unauthorized("No token found in the request.");
                }

                var token = authorizationHeader.Replace("Bearer ", "");
                // Optionally, you can implement token invalidation logic.
                return Ok("Logged out successfully!");
            }
        }

        public class LoginRequest
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
    }

}
