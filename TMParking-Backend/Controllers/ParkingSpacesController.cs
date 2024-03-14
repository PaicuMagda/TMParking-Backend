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
            return Ok(new { Message = "Parking Spaces was successfully deleted !"});
        }


        [HttpPut("update-parking-spaces/{id}")]
        public async Task<IActionResult> UpdateParkingSpaces(int id, ParkingSpaces parkingSpaces)
        {
            var parkingSpacesToUpdate = await _dbContextTMParking.ParkingSpaces.FirstOrDefaultAsync(p => p.ParkingSpacesId == id);

            if (parkingSpacesToUpdate == null)
            {
                return NotFound("Parking Spaces not found !");
            }

            parkingSpacesToUpdate.Name = parkingSpaces.Name;
            parkingSpacesToUpdate.Address=parkingSpaces.Address;
            parkingSpacesToUpdate.AvailableParkingSpaces = parkingSpaces.AvailableParkingSpaces;
            parkingSpacesToUpdate.IsCargoVehicleAccepted =parkingSpaces.IsCargoVehicleAccepted;
            parkingSpacesToUpdate.IsPersonalVehicleAccepted=parkingSpaces.IsPersonalVehicleAccepted;
            parkingSpacesToUpdate.IsPublicTransportAccepted=parkingSpaces.IsPublicTransportAccepted;
            parkingSpacesToUpdate.IsAgriculturalMachineryAccepted=parkingSpaces.IsAgriculturalMachineryAccepted;    
            parkingSpacesToUpdate.ImageProfile=parkingSpaces.ImageProfile;
            parkingSpacesToUpdate.LeasePermit=parkingSpaces.LeasePermit;
            parkingSpacesToUpdate.StartDate = parkingSpaces.StartDate;
            parkingSpacesToUpdate.EndDate = parkingSpaces.EndDate;
            parkingSpacesToUpdate.AddedDate = parkingSpaces.AddedDate;
            parkingSpacesToUpdate.IsFree= parkingSpaces.IsFree;
            parkingSpacesToUpdate.IsVideoSurveilance=parkingSpaces.IsVideoSurveilance;
            parkingSpacesToUpdate.Description=parkingSpaces.Description;
            parkingSpacesToUpdate.IsDraft=parkingSpaces.IsDraft;
            parkingSpacesToUpdate.PaymentPerHour = parkingSpaces.PaymentPerHour;
            parkingSpacesToUpdate.PaymentPerDay=parkingSpaces.PaymentPerDay;
            parkingSpacesToUpdate.PaymentForSubscription=parkingSpaces.PaymentForSubscription;
            parkingSpacesToUpdate.IsVerifiedByAdmin = parkingSpaces.IsVerifiedByAdmin;
            parkingSpaces.SomethingIsWrong=parkingSpaces.SomethingIsWrong;

            await _dbContextTMParking.SaveChangesAsync();

            return Ok(new { Message = "Parking Spaces update successfully !" });
        }
    }
}
