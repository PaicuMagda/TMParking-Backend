
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
                Include(v => v.Vehicle).
                Select(r => new
                {
                    reservationId = r.ReservationId,
                    StartTime = r.StartDate,
                    EndTime = r.EndDate,
                    ParkingSpaceName = r.ParkingSpaceModel.ParkingSpaces.Name,
                    ParkingLotName = r.ParkingSpaceModel.Name,
                    VehicleRegisteredNumber = r.Vehicle.vehicleIdentificationNumber,
                    VehicleOwner = r.Vehicle.VehicleOwner.FullName,
                    VehicleOwnerId = r.Vehicle.VehicleOwner.UserId,
                    ProviderParkingSpace = r.ParkingSpaceModel.ParkingSpaces.ParkingSpacesOwner.FullName

                })
                .ToListAsync();

            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] Reservation reservation)
        {
            var vehicle = _dbContextTMParking.Vehicles.FirstOrDefault(v => reservation.VehicleRegistrationNumber == v.vehicleIdentificationNumber);
            var parkingLot = _dbContextTMParking.ParkingSpacesForOneParkingSpace.FirstOrDefault(p => reservation.SpaceModelName == p.Name);

            if (parkingLot != null)
            {
                reservation.VehicleId = vehicle.VehicleId;
                bool reservationExists = _dbContextTMParking.Reservations.
                    Any(r => r.SpaceModelName == reservation.SpaceModelName && r.StartDate < reservation.EndDate && r.EndDate > reservation.StartDate);

                if (reservationExists)

                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "This time slot is already booked."
                    });

                }

                reservation.ParkingSpaceModelId = parkingLot.ParkingSpaceModelId;
                await _dbContextTMParking.Reservations.AddAsync(reservation);
                await _dbContextTMParking.SaveChangesAsync();
                return Ok(new { Message = "Reservation added successfully!" });
            }
            else
            {
                return BadRequest("Parking lot not found!");
            }
        }

        [HttpGet("{userId}/reservations")]
        public async Task<ActionResult> GetReservationByUserId(int userId)
        {
            var reservations = _dbContextTMParking.Reservations.Where(u => u.Vehicle.VehicleOwnerId == userId).
                 Include(v => v.Vehicle).
                Select(r => new
                {
                    reservationId = r.ReservationId,
                    startDate = r.StartDate,
                    endDate = r.EndDate,
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

        [HttpGet("reservations/{parkingModelId}")]
        public async Task<ActionResult> GetReservationsByParkingLotId(int parkingModelId)
        {
            var reservations = _dbContextTMParking.Reservations.Where(r => r.ParkingSpaceModelId == parkingModelId).
                Include(r => r.Vehicle).Select(r => new
                {
                    reservationId = r.ReservationId,
                    startDate = r.StartDate,
                    endDate = r.EndDate,
                    ParkingLotName = r.ParkingSpaceModel.Name,
                    reservationType = r.ReservationType
                }).ToList();

            if (reservations == null)
            {
                return NotFound();
            }

            return Ok(reservations);
        }


        [HttpGet("{parkingSpaceId}/reservationsForABigParkingSpace")]
        public async Task<ActionResult> GetReservationByParkingSpace(int parkingSpaceId)
        {
            var reservations = _dbContextTMParking.Reservations.Where(u => u.ParkingSpaceModel.ParkingSpacesId == parkingSpaceId).
                 Include(v => v.Vehicle).
                Select(r => new
                {
                    reservationId = r.ReservationId,
                    startDate = r.StartDate,
                    endDate = r.EndDate,
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

            if (reservation == null) { return NotFound(); }

            _dbContextTMParking.Reservations.Remove(reservation);
            _dbContextTMParking.SaveChanges();
            return Ok(new { Message = "Parking Spaces was successfully deleted !" });
        }
    }
}
