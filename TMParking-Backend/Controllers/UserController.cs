
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TMParking_Backend.Data;
using TMParking_Backend.Helper;
using TMParking_Backend.Models;
using TMParking_Backend.Models.Dto;

namespace TMParking_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;

        public UserController(DbContextTMParking dbContextTMParking)
        {
            _dbContextTMParking = dbContextTMParking;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _dbContextTMParking.Users.ToListAsync();
            return Ok(users);

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User newUser)
        {
            if (newUser == null)
                return BadRequest();

            if (await EmailAlreadyExistsAsync(newUser.Email)) return BadRequest(new { Message = "Email already exists !" });
            if (await UsernameAlreadyExistsAsync(newUser.Username)) return BadRequest(new { Message = "Username already exists !" });

            var pass = CheckPasswordStrength(newUser.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });

            newUser.Password = PasswordHasher.HashPassword(newUser.Password);
            newUser.Role = "User";
            newUser.Token = "";
            await _dbContextTMParking.Users.AddAsync(newUser);
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new { Message = "User Registered !" });
        }

        private Task<bool> EmailAlreadyExistsAsync(string email)
        => _dbContextTMParking.Users.AnyAsync(x => x.Email == email);

        private Task<bool> UsernameAlreadyExistsAsync(string username)
        => _dbContextTMParking.Users.AnyAsync(x => x.Username == username);



        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _dbContextTMParking.Users.FirstOrDefaultAsync(x => x.Username == userObj.Username);

            if (user == null)
                return NotFound(new { Message = "User Not Found !" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Incorect Password !" });
            }

            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
            await _dbContextTMParking.SaveChangesAsync();

            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Message = "Authentication successful",
            });
        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret...");
            var identity = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
                });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials,
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);
            var tokenInUser = _dbContextTMParking.Users.Any(a => a.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes("veryverysecret...");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");
            return principal;
        }

        private static string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 9)
                sb.Append("Minimum password length should be 8 !" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric !" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',.',','\\','=']"))
                sb.Append("Passsword should contain special chars !" + Environment.NewLine);
            return sb.ToString();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenApiDto tokenApiDto)
        {
            if (tokenApiDto is null)
                return BadRequest("Invalid Client Request");
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;
            var principal = GetPrincipleFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = await _dbContextTMParking.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid Request");
            var newAccessToken = CreateJwt(user);
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new TokenApiDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,

            });
        }
    }
}
