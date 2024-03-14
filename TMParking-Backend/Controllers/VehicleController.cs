using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMParking_Backend.Data;
using TMParking_Backend.Models;

namespace TMParking_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;

        public VehicleController(DbContextTMParking dbContextTMParking)
        {
            _dbContextTMParking = dbContextTMParking;
        }


        [HttpGet("vehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _dbContextTMParking.Vehicles.Include(v => v.VehicleOwner).ToListAsync();
            return Ok(vehicles);
        }


        [HttpPost("register-vehicle")]
        public async Task<IActionResult> RegisterVehicle([FromBody] Vehicle newVehicle)
        {
            if (newVehicle == null)
                return BadRequest();

            await _dbContextTMParking.Vehicles.AddAsync(newVehicle);
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new { Message = "Vehicle Registered !" });
        }


        [HttpGet("{userId}/vehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles(int userId)
        {
            var user = await _dbContextTMParking.Users.Include(u => u.Vehicles).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }
            return user.Vehicles.ToList();
        }


        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> DeleteVehicleById(int vehicleId)
        {
            Vehicle vehicle = await _dbContextTMParking.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == vehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }
            _dbContextTMParking.Vehicles.Remove(vehicle);
            _dbContextTMParking.SaveChanges();
            return Ok(new { Message = "Vehicle was successfully deleted !" });
        }

        [HttpPut("update-vehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle) {

            Vehicle vehicleToUpdate = await _dbContextTMParking.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == id);

            if (vehicleToUpdate == null)
            {
                return NotFound("Vehicle not found !");
            }
            vehicleToUpdate.ImageProfileBase64 = vehicle.ImageProfileBase64;
            vehicleToUpdate.Make = vehicle.Make;
            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.Color = vehicle.Color;
            vehicleToUpdate.Year= vehicle.Year;
            vehicleToUpdate.vehicleIdentificationNumber = vehicle.vehicleIdentificationNumber;
            vehicleToUpdate.vehicleRegistrationCertificateBase64 = vehicle.vehicleRegistrationCertificateBase64;
            vehicleToUpdate.IsVerifiedByAdmin = vehicle.IsVerifiedByAdmin;
            vehicleToUpdate.SomethingIsWrong = vehicle.SomethingIsWrong;
            vehicleToUpdate.DateAdded= vehicle.DateAdded;   

            await _dbContextTMParking.SaveChangesAsync();

            return Ok(new { Message = "Vehicle update successfully." });
        }
    }
}
