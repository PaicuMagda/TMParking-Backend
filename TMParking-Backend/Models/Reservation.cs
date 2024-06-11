using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMParking_Backend.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ? EndDate { get; set; }
        public string PaymentMethod { get; set; }  
        public string VehicleRegistrationNumber { get; set; } 
        public string SpaceModelName { get; set; }
        public float PriceToPay { get; set; }
        public string ReservationType { get; set; }
        public int NumberOfMonths { get; set; }
        public int bigParkingSpacesId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User Owner { get; set; }

        [ForeignKey("ParkingSpaceModel")]
        public int ParkingSpaceModelId { get; set; }
        public ParkingSpaceModel ParkingSpaceModel { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
