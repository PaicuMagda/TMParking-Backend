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
            return Ok(new { Message = "Parking spaces successfully added !" });
        }

        [HttpGet("{userId}/parking-spaces")]
        public async Task<ActionResult<IEnumerable<ParkingSpaces>>> GetMyParkingSpaces(int userId)
        {
            var user = await _dbContextTMParking.Users.Include(u=>u.ParkingSpaces).FirstOrDefaultAsync(u=>u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            return user.ParkingSpaces.ToList();
        
        }

        [HttpGet("{parkingSpacesId}/parkingSpaces")]
        public async Task<IActionResult> GetParkingSpacesById(int parkingSpacesId)
        { 
           ParkingSpaces parkingSpaces = await _dbContextTMParking.ParkingSpaces.FirstOrDefaultAsync(p=>p.ParkingSpacesId== parkingSpacesId);

            if (parkingSpaces == null)
            {
                return NotFound();
            }
        return Ok(parkingSpaces);
        }

        [HttpDelete("{parkingSpacesId}")]
        public async Task<IActionResult> DeleteParkingSpaces(int parkingSpacesId)
        {
            ParkingSpaces parkingSpaces = await _dbContextTMParking.ParkingSpaces.FirstOrDefaultAsync(p => p.ParkingSpacesId == parkingSpacesId);

            if (parkingSpaces == null)
            {
                return NotFound();
            }

            _dbContextTMParking.ParkingSpaces.Remove(parkingSpaces);
            _dbContextTMParking.SaveChanges();
            return Ok(new { Message = "Parking Spaces delete successfully !"});
        }
    
    }
}
