﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMParking_Backend.Data;
using TMParking_Backend.Models;

namespace TMParking_Backend.Controllers
{
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
            var vehicles = await _dbContextTMParking.Vehicles.Include(v=>v.VehicleOwner).ToListAsync();
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
    }
}