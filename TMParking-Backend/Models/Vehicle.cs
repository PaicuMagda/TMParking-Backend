using System.ComponentModel.DataAnnotations;

namespace TMParking_Backend.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string ImageProfileBase64 { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }   
        public string vehicleIdentificationNumber { get; set; }
        public string vehicleRegistrationCertificateBase64 { get; set; }
        public bool IsVerifiedByAdmin { get; set; } 
        public bool SomethingIsWrong { get; set; }
        public DateTime DateAdded { get; set; }
        public int VehicleOwnerId { get; set; }
        public User VehicleOwner { get; set; }

    }
}
