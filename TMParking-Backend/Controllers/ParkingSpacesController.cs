using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMParking_Backend.Data;
using TMParking_Backend.Models;

namespace TMParking_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParkingSpacesController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;

        public ParkingSpacesController(DbContextTMParking dbContextTMParking)
        {
            _dbContextTMParking = dbContextTMParking;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParkingSpaces()
        {
            var parkingSpaces = await _dbContextTMParking.ParkingSpaces.Include(p => p.ParkingSpacesOwner).ToListAsync();
            return Ok(parkingSpaces);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterParkingSpaces([FromBody] ParkingSpaces parkingSpaces)
        {
            if (parkingSpaces == null)
                return BadRequest();

            await _dbContextTMParking.ParkingSpaces.AddAsync(parkingSpaces);
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new { Message= "Parking spaces successfully added !" });
        }
    }

}
