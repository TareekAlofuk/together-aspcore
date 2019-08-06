using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using together_aspcore.App.User;
using together_aspcore.Shared;

namespace together_aspcore.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private TogetherDbContext _context;
        private IConfiguration _config;


        public UserController(TogetherDbContext context,
            IConfiguration configuration
        )
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet("generate")]
        public ActionResult Generate([FromQuery] string password)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>(
                new OptionsWrapper<PasswordHasherOptions>(
                    new PasswordHasherOptions()
                    {
                        CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
                    })
            );
            return Ok(new
            {
                Hash = hasher.HashPassword(null, password)
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] LoginRequestModel loginRequestModel)
        {
            var user = await _context.Users.Where(x => x.Username == loginRequestModel.Username)
                .FirstOrDefaultAsync();
            if (user == null) return BadRequest();
            PasswordHasher<User> hasher = new PasswordHasher<User>(
                new OptionsWrapper<PasswordHasherOptions>(
                    new PasswordHasherOptions()
                    {
                        CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
                    })
            );

            if (hasher.VerifyHashedPassword(user,
                    user.Password,
                    loginRequestModel.Password) != PasswordVerificationResult.Success) return BadRequest();


            var claims = new List<Claim>
            {
                new Claim("UserType", user.UserType),
                new Claim("Username", user.Username),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                null, DateTime.Now.AddDays(10),
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = token.ValidTo
            });
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public ActionResult RefreshToken()
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                User.Claims,
                null, DateTime.Now.AddDays(10),
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = token.ValidTo
            });
        }
    }

    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}