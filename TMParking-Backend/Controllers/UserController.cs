using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TMParking_Backend.Data;
using TMParking_Backend.Helper;
using TMParking_Backend.Helpers;
using TMParking_Backend.Models;
using TMParking_Backend.Models.Dto;
using TMParking_Backend.UtilityService;

namespace TMParking_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UserController(DbContextTMParking dbContextTMParking, IConfiguration configuration, IEmailService emailService)
        {
            _dbContextTMParking = dbContextTMParking;
            _configuration = configuration;
            _emailService = emailService;
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
                RefreshToken = newRefreshToken
            });
        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret...");
            var identity = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString())
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
            if (password.Length <= 8)
                sb.Append("Minimum password length should be 8 !" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric !" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',.',','\\','=']"))
                sb.Append("Passsword should contain special chars !" + Environment.NewLine);
            return sb.ToString();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenApiDto tokenApiDto)
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
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,

            });
        }

        [HttpPost("send-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _dbContextTMParking.Users.FirstOrDefaultAsync(a => a.Email == email);

            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Email doesn't exist "
                });
            }
            var nameUser = user.FirstName;
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddDays(1);
            string from = _configuration["EmailSettings:From"];
            var emailModel = new EmailModel(email, "ResetPassword!!", EmailBody.EmailStringBody(email, emailToken, nameUser));
            _emailService.SendEmail(emailModel);
            _dbContextTMParking.Entry(user).State = EntityState.Modified;
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Email Sent !"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = await _dbContextTMParking.Users.AsNoTracking().FirstOrDefaultAsync(a => a.Email == resetPasswordDto.Email);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Doesn't Exist "
                });
            }
            var tokenCode = user.ResetPasswordToken;
            DateTime emailTokenExpiry = user.ResetPasswordExpiry;
            if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Invalid Reset link"
                });
            }
            user.Password = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
            _dbContextTMParking.Entry(user).State = EntityState.Modified;
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Password Reset Successfully"

            });
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            var users = await _dbContextTMParking.Users
                .ToListAsync();
            return Ok(users);
        }

        //[Authorize]
        [HttpGet("admin-page")]
        public async Task<ActionResult<User>> GetUsersAdminPage()
        {
            var users = await _dbContextTMParking.Users.Include(u => u.Vehicles).Select(u =>
            new {
                userId = u.UserId,
                email = u.Email,
                firstName = u.FirstName,
                lastName = u.LastName,
                address = u.Address,
                dateOfBirth = u.DateOfBirth,
                role = u.Role,
                zipCode = u.ZipCode,
                state = u.State,
                isActive = u.IsActive,
                phone = u.Phone,
                pnc = u.PNC,
                imageUrl = u.ImageUrl,
                vehiclesRegistered = u.Vehicles.Select(v => v.vehicleIdentificationNumber).ToList(),

            })
                .ToListAsync();
            return Ok(users);
        }

        [HttpPut("update-user/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User userForUpdate)
        {
            var user = await _dbContextTMParking.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User Not Found!");
            }

            user.FirstName = userForUpdate.FirstName;
            user.LastName = userForUpdate.LastName;
            user.Password = userForUpdate.Password;
            user.Role = "User";
            user.Address = userForUpdate.Address;
            user.ZipCode = userForUpdate.ZipCode;
            user.State = userForUpdate.State;
            user.IsActive = userForUpdate.IsActive;
            user.Phone = userForUpdate.Phone;
            user.DateOfBirth = userForUpdate.DateOfBirth;
            user.PNC = userForUpdate.PNC;
            user.ImageUrl = userForUpdate.ImageUrl;

            bool existsEmail = await EmailAlreadyExistsAsync(userForUpdate.Email);

            if ((userForUpdate.Email !=  user.Email) && existsEmail) return BadRequest(new { Message = "Email already exists !" });
       
            user.Email = userForUpdate.Email;

            await _dbContextTMParking.SaveChangesAsync();

            return Ok(new { Message = "User updated successfully." });

        }

        [HttpPut("update-my-account/{id}")]
        public async Task<ActionResult<User>> UpdateMyAccount(int id, [FromBody] User userForUpdate)
        {
            var user = await _dbContextTMParking.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User Not Found!");
            }

            user.FirstName = userForUpdate.FirstName;
            user.LastName = userForUpdate.LastName;
            user.Password = userForUpdate.Password;
            user.Role = "User";
            user.Address = userForUpdate.Address;
            user.ZipCode = userForUpdate.ZipCode;
            user.State = userForUpdate.State;
            user.IsActive = userForUpdate.IsActive;
            user.Phone = userForUpdate.Phone;
            user.DateOfBirth = userForUpdate.DateOfBirth;
            user.PNC = userForUpdate.PNC;
            user.ImageUrl = userForUpdate.ImageUrl;

            var pass = CheckPasswordStrength(userForUpdate.Password);
            bool existsEmail = await EmailAlreadyExistsAsync(userForUpdate.Email);
            bool existsUsername = await UsernameAlreadyExistsAsync(userForUpdate.Username);

            if ((userForUpdate.Email != user.Email) && existsEmail) return BadRequest(new { Message = "Email already exists !" });
            if (userForUpdate.Username != user.Username && existsUsername) return BadRequest(new { Message = "Username already exists !" });

            user.Email = userForUpdate.Email;
            user.Username = userForUpdate.Username;
            user.Password = PasswordHasher.HashPassword(userForUpdate.Password);

            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });

            await _dbContextTMParking.SaveChangesAsync();

            return Ok(new { Message = "User updated successfully." });

        }


        [HttpGet("{userId}/user-account")]
        public async Task<ActionResult> GetUserLogged(int userId)
        {
            var user = await _dbContextTMParking.Users.Include(u => u.Vehicles).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) 
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserById(int userId)
        { 
          User user = await _dbContextTMParking.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }
            _dbContextTMParking.Users.Remove(user);
            _dbContextTMParking.SaveChanges();
            return Ok(new { Message = "User Account was successfully deleted !" });
        }
    }
}
