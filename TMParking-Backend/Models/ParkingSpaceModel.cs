using System.ComponentModel.DataAnnotations;
namespace TMParking_Backend.Models
{
    public class ParkingSpaceModel
    {
        [Key]
        public int ParkingSpaceModelId { get; set; }
        public string Name { get; set; }
        public string Availability { get; set; }
        public int ParkingSpacesId { get; set; }
        public ParkingSpaces ParkingSpaces {get; set;}
        public ICollection<Reservation> Reservations { get;}=new List<Reservation>();
    }
}
