using KontaktAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KontaktAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ContactDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountController(ContactDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        /// <summary>
        /// metoda do generowania jwt tokenu
        /// </summary>
        /// <param name="loginData">dane logowania</param>
        /// <returns>zwraca token jwt</returns>
        public string GenerateJwt(LoginFromClient loginData)
        {
            var user = _dbContext.Users
                .Include(m => m.Role)
                .FirstOrDefault(n => n.Email == loginData.Email);

            if (user == null)
            {
                return "Zły email lub hasło";
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginData.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return "Zły email lub hasło";
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.RoleName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDay);


            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
               _authenticationSettings.JwtIssuer,
               claims,
               expires: expires,
               signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }



        /// <summary>
        /// akcja do rejestracji użytkownika
        /// </summary>
        /// <param name="user">dane użytkownika do rejestracji</param>
        /// <returns>typ żądnia czy jest ok czy nie</returns>
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] UserFromClient user)
        {
            var roleToSign = _dbContext.Roles.FirstOrDefault();
            var UserSave = new User
            {
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = 1,             
            };
            var hashedPass = _passwordHasher.HashPassword(UserSave, user.PasswordHash);
            UserSave.PasswordHash = hashedPass;
            var uniqeMail = _dbContext.Users.FirstOrDefault(n => n.Email == user.Email);
            if (uniqeMail != null)
            {
                return BadRequest("Taki email już istnieje");
            }
            _dbContext.Users.Add(UserSave);
            _dbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// akcja do logowania użytkowniak 
        /// </summary>
        /// <param name="loginData">dane logowania</param>
        /// <returns>token jwt</returns>
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginFromClient loginData )
        {
            string token = GenerateJwt(loginData);

            if(token.Length <24)
            {
                return BadRequest("Zły email lub hasło");
            }

            return Ok(token);
        }
    }
}
