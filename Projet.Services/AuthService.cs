using Project.Entities;
using Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;

namespace Project.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUtilisateurService utilisateurService, IOptions<JwtSettings> jwtSettings)
        {
            _utilisateurService = utilisateurService;
            _jwtSettings = jwtSettings.Value;
        }

        public Utilisateur Authenticate(string email, string password)
        {
            var user = _utilisateurService.GetUsers().FirstOrDefault(u => u.Email == email);
            Console.WriteLine(user);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.MotDePasse))
                return null;

            return user;
        }


        public string GenerateToken(Utilisateur user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            // Define claims for the token
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            // Create the token descriptor with claims, expiry, signing credentials, etc.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            // Generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the generated token
            return tokenHandler.WriteToken(token);
        }

        /*public string GenerateToken(Utilisateur user)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiryMinutes),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }*/
        }
    }

