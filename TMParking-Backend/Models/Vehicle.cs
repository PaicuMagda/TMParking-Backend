using Org.BouncyCastle.Utilities.Encoders;
using System.ComponentModel.DataAnnotations;

namespace TMParking_Backend.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string imageUrl { get; set; }    
        public string make { get; set; }
        public string model { get; set; }       
        public string color { get; set; }
        public int year { get; set; }
        public int UserId { get; set; }
        public User owner { get; set; } 
        public string vehicleIdentificationNumber { get; set; } 
        public string vehicleRegistrationCertificate { get; set; } 

    }
}
