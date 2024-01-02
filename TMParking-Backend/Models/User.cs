using System.ComponentModel.DataAnnotations;
using TMParking_Backend.Enums;

namespace TMParking_Backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public RolesEnum Role { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string State { get;set; }
        public bool isActive { get; set; }  
        public int PhoneNumber { get; set; }    
        public DateTime dateOfBirth { get; set; }   
        public int PersonalNumericalNumber { get; set; }    
        public bool licenseValid { get; set; }  
        public string imageUrl { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string ResetPasswordToken    { get; set; }
        public DateTime ResetPasswordExpiry { get; set; }
    }
}
