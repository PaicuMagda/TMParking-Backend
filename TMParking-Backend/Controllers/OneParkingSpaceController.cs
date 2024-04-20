
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
            return Ok(new { Message = "Parking Spaces Added Sucessfully!" });
        }

        [HttpGet("parking-lots")]
        public async Task<IActionResult> GetAllParkingLotsByParkingSpacesId(int parkingSpacesId )
        {
            var parkingSpaces = await _dbContextTMParking.ParkingSpaces.Include(p=>p.ParkingSpaceForOneParking)
                .FirstOrDefaultAsync(p => p.ParkingSpacesId == parkingSpacesId);
         
            if (parkingSpaces == null)
            {
                return BadRequest();
            }
            else return Ok(parkingSpaces.ParkingSpaceForOneParking);
        }

        
    }
}
