
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMParking_Backend.Data;
using TMParking_Backend.Models;

namespace TMParking_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;
        public ReservationController(DbContextTMParking dbContextTMParking)
        {
            _dbContextTMParking = dbContextTMParking;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _dbContextTMParking.Reservations.
                Include(v=>v.Vehicle).
                Select(r => new 
                { 
                    ReservationId=r.ReservationId,
                    StartTime=r.StartDate,
                    EndTime=r.EndDate,
                    ParkingSpaceName=r.ParkingSpaceModel.ParkingSpaces.Name,
                    ParkingLotName=r.ParkingSpaceModel.Name,
                    VehicleRegisteredNumber=r.Vehicle.vehicleIdentificationNumber,
                    VehicleOwner=r.Vehicle.VehicleOwner.FullName,
                    VehicleOwnerId=r.Vehicle.VehicleOwner.UserId,
                    ProviderParkingSpace=r.ParkingSpaceModel.ParkingSpaces.ParkingSpacesOwner.FullName
                })
                .ToListAsync();

            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] Reservation reservation)
        {

            if (reservation == null || string.IsNullOrEmpty(reservation.VehicleRegistrationNumber) || string.IsNullOrEmpty(reservation.SpaceModelName))
                return BadRequest("Invalid reservation data");

            var vehicle = _dbContextTMParking.Vehicles.FirstOrDefault(v => reservation.VehicleRegistrationNumber == v.vehicleIdentificationNumber);
            var parkingLot = _dbContextTMParking.ParkingSpacesForOneParkingSpace.FirstOrDefault(p=>reservation.SpaceModelName == p.Name);

            if (vehicle != null)
            {
                reservation.VehicleId = vehicle.VehicleId;
                reservation.ParkingSpaceModelId=parkingLot.ParkingSpaceModelId;
                await _dbContextTMParking.Reservations.AddAsync(reservation);
                await _dbContextTMParking.SaveChangesAsync();
                return Ok(new { Message = "Reservation added successfully!" });
            }
            else
            {
                return BadRequest("Vehicle not found!");
            }
        }

        [HttpGet("{userId}/reservations")]
        public async Task<ActionResult> GetReservationById(int userId)
        {
            var reservations = _dbContextTMParking.Reservations.Where(u => u.Vehicle.VehicleOwnerId == userId).
                 Include(v => v.Vehicle).
                Select(r => new
                {
                    ReservationId = r.ReservationId,
                    StartTime = r.StartDate,
                    EndTime = r.EndDate,
                    ParkingSpaceName = r.ParkingSpaceModel.ParkingSpaces.Name,
                    ParkingLotName = r.ParkingSpaceModel.Name,
                    VehicleRegisteredNumber = r.Vehicle.vehicleIdentificationNumber,
                    VehicleOwner = r.Vehicle.VehicleOwner.FullName,
                    VehicleOwnerId = r.Vehicle.VehicleOwner.UserId,
                    ProviderParkingSpace = r.ParkingSpaceModel.ParkingSpaces.ParkingSpacesOwner.FullName
                }).ToList();

            if (reservations == null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            Reservation reservation = await _dbContextTMParking.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if(reservation == null) { return NotFound(); }

            _dbContextTMParking.Reservations.Remove(reservation);
            _dbContextTMParking.SaveChanges();
            return Ok(new { Message = "Parking Spaces was successfully deleted !" });
        }
    }
}
