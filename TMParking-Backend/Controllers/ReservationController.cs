
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
                    StartTime=r.StartTime,
                    EndTime=r.EndTime,
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
        public async Task<IActionResult> AddReservation([FromBody] Reservation reservation) {

            if (reservation == null)
                return BadRequest();

            await _dbContextTMParking.Reservations.AddAsync(reservation);
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new { Message ="Reservaton added sucessfully!"});
        }
    }
}
