using Microsoft.AspNetCore.Http;
using QuoteQuiz.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuoteQuiz.Application.Contracts.Services;
using QuoteQuiz.Application.Services;
using QuoteQuiz.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuoteQuiz.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly QuizDbContext _db;
        private readonly IUserService _users;
        private readonly IConfiguration _config;

        public AuthController(IUserService users, QuizDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
            _users = users;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var user = await _users.GetByEmail(req.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            var claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Name, user.Username),
                                        new Claim("userId", user.Id.ToString())
                                    };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                username = user.Username,
                roles = roles,
                userId = user.Id
            });
        }
    }

}
