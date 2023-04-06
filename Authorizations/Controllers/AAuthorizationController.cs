using Authorizations.Handler;
using Authorizations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorizations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AAuthorizationController : ControllerBase
    {
        private readonly EmployeeDbContext _context;
        private readonly JWTSettings _jwtSettings;
        public AAuthorizationController(EmployeeDbContext context, IOptions<JWTSettings> options)
        {
            _context = context;
            _jwtSettings = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody] JWTLogin jWTLogin)
        {
            var _user = _context.employees.FirstOrDefault(o => o.Email == jWTLogin.Email && o.FirstName == jWTLogin.password);
            if (_user == null)
                return Unauthorized();

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name,_user.LastName),
                        new Claim(ClaimTypes.Role,_user.Role),
                        new Claim(ClaimTypes.Email, jWTLogin.Email)
                    }
                ),
                Expires = DateTime.Now.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            return Ok(finaltoken);
        }
    }
}
