using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PlantsDetection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public static User user = new User();
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }
        [HttpPost("registration")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            CreateHashPassword(request.Password, out byte[] hashPassword, out byte[] saltPassword);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Phone = request.Phone;
            user.Country = request.Country;
            user.Email = request.Email;
            user.PasswordHash = hashPassword;
            user.PasswordSalt = saltPassword;
            return Ok(user);
        }

        private void CreateHashPassword(string password,out byte[]hashPassword, out byte[] saltPassword) 
        {
            using (var hmac = new HMACSHA512()) 
            {
                saltPassword = hmac.Key;
                hashPassword=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private bool VerifyHashPassword(string password, byte[] hashPassword, byte[] saltPassword)
        {
            using (var hmac = new HMACSHA512(saltPassword))
            {
                
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(hashPassword);

            }
         }

        [HttpPost("login")]
        public async Task<ActionResult<User>> login(UserDTO request)
        {
            if (user.UserName != request.UserName )
            {
                return BadRequest("User Not Found");
            }
            if(!VerifyHashPassword(request.Password,user.PasswordHash,user.PasswordSalt))
            {
                return BadRequest("Wrong Password.");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
                
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            
            var cred= new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
