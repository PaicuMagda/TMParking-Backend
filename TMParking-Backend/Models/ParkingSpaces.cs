using System.ComponentModel.DataAnnotations;
namespace TMParking_Backend.Models
{
    public class ParkingSpaces
    {
        [Key]
        public int ParkingSpacesId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int AvailableParkingSpaces { get; set; }
        public bool IsCargoVehicleAccepted { get; set; }
        public bool IsPersonalVehicleAccepted { get; set; } 
        public bool IsPublicTransportAccepted { get; set; }
        public bool IsAgriculturalMachineryAccepted { get; set; }
        public string ImageProfile { get; set; }
        public string LeasePermit { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsFree { get; set; }
        public bool IsVideoSurveilance { get; set; }
        public string Description { get; set; }
        public bool IsDraft { get; set; }
        public float PaymentPerHour { get;set; }
        public float PaymentPerDay { get;set; }
        public float PaymentForSubscription { get; set; }
        public bool IsVerifiedByAdmin { get; set; }
        public bool SomethingIsWrong { get; set; }
        public bool UndergroundParkingLots { get; set; }
        public bool MultistoreyCarPark { get; set; }
        public bool PaidParking { get; set; }
        public string Area { get; set; }    
        public int ParkingSpacesOwnerId { get; set; }
        public User ParkingSpacesOwner { get; set; }
        public ICollection<ParkingSpaceModel> ParkingSpaceForOneParking { get;} = new List<ParkingSpaceModel>();
    }
}
