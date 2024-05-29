
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMParking_Backend.Data;
using TMParking_Backend.Models;

namespace TMParking_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OneParkingSpaceController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;

        public OneParkingSpaceController(DbContextTMParking dbContextTMParking)
        {
            _dbContextTMParking = dbContextTMParking;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParkingSpacesFromOneParking()
        {
            var parkingSpaces = await _dbContextTMParking.ParkingSpacesForOneParkingSpace.ToListAsync();
            return Ok(parkingSpaces);
        }

        [HttpPost]
        public async Task<IActionResult> AddOneParkingSpace([FromBody] ParkingSpaceModel parkingSpace)
        {
            if (parkingSpace == null) return BadRequest();

            await _dbContextTMParking.ParkingSpacesForOneParkingSpace.AddAsync(parkingSpace);
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new { Message = "Parking Spaces Added Successfully!" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOneParkingSpace(string parkingSpaceName, int parkingSpaceId)
        {
            ParkingSpaceModel parkingLot = await _dbContextTMParking.ParkingSpacesForOneParkingSpace.FirstOrDefaultAsync(p => p.Name == parkingSpaceName && p.ParkingSpacesId == parkingSpaceId);

            if (parkingLot == null) return NotFound();

            _dbContextTMParking.ParkingSpacesForOneParkingSpace.Remove(parkingLot);
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new { Message = "Parking Spaces was successfully deleted!" });
        }

        [HttpGet("parking-lots")]
        public async Task<IActionResult> GetAllParkingLotsByParkingSpacesId(int parkingSpacesId )
        {
            var parkingSpaces = await _dbContextTMParking.ParkingSpacesForOneParkingSpace.Include(p => p.Reservations).Include(p=>p.ParkingSpaces).
                Where(p=>p.ParkingSpacesId == parkingSpacesId).Select(p=> 
                new { 
                   parkingLotId=p.ParkingSpaceModelId,
                   name=p.Name,
                   availability=p.Availability,
                   parkingSpacesId=p.ParkingSpacesId,
                   reservations = p.Reservations,
                   startDate=p.ParkingSpaces.StartDate,
                   endDate=p.ParkingSpaces.EndDate, 
                })
                .ToListAsync();
         
            if (parkingSpaces == null)
            {
                return BadRequest();
            }
            else return Ok(parkingSpaces);
        }
    }
}
