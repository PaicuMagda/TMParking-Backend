using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
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
            var vehicles = await _dbContextTMParking.Vehicles.Include(v => v.VehicleOwner).Select(v => new 
            { 
                vehicleId =v.VehicleId,
                make=v.Make,
                model=v.Model,
                color=v.Color,
                year=v.Year,
                imageProfileBase64=v.ImageProfileBase64,
                vehicleOwner =v.VehicleOwner.FullName,
                vehicleIdentificationNumber=v.vehicleIdentificationNumber,
                vehicleRegistrationCertificateBase64 =v.vehicleRegistrationCertificateBase64,
                dateAdded=v.AddedDate,

            }).ToListAsync();
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
        public async Task<ActionResult> GetVehicles(int userId)
        {
            var vehicles = await _dbContextTMParking.Vehicles.Include(v => v.VehicleOwner).Where(v=> v.VehicleOwnerId == userId).Select(
                v=> new
            {
                ownerId = v.VehicleOwnerId,
                vehicleId = v.VehicleId,
                make = v.Make,
                model = v.Model,
                color = v.Color,
                year = v.Year,
                imageProfileBase64 = v.ImageProfileBase64,
                vehicleOwner = v.VehicleOwner.FullName,
                vehicleIdentificationNumber = v.vehicleIdentificationNumber,
                vehicleRegistrationCertificateBase64 = v.vehicleRegistrationCertificateBase64,
                dateAdded=v.AddedDate

                }).ToListAsync();

            if (vehicles == null)
            {
                return NotFound();
            }
            return Ok(vehicles);
        }

        [HttpGet("{idVehicle}")]
        public async Task<IActionResult> GetVehicleById(int idVehicle) 
        {
            var vehicle = await _dbContextTMParking.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == idVehicle);

            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
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
            vehicleToUpdate.AddedDate= vehicle.AddedDate;   

            await _dbContextTMParking.SaveChangesAsync();

            return Ok(new { Message = "Vehicle update successfully." });
        }
    }
}
