using System.ComponentModel.DataAnnotations;
namespace TMParking_Backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string ResetPasswordToken    { get; set; }
        public DateTime ResetPasswordExpiry { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string   State { get; set; }
        public bool IsActive { get; set; }
        public int Phone { get; set; }
        public string DateOfBirth { get; set; }
        public int PNC { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AddedDate { get; set; }
        public ICollection<Vehicle> Vehicles { get;}=new List<Vehicle>();
        public ICollection<ParkingSpaces> ParkingSpaces { get;}=new List<ParkingSpaces>();
        public ICollection<Reservation>Reservations { get; } = new List<Reservation>();
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
