using System.ComponentModel.DataAnnotations;

namespace TMParking_Backend.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string ImageUrl { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int OwnerId { get; set; }    
        public int vehicleIdentificationNumber { get; set; }
        public string vehicleRegistrationCertificate { get; set; }

    }
}
