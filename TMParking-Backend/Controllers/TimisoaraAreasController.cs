
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMParking_Backend.Data;
using TMParking_Backend.Models;

namespace TMParking_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TimisoaraAreasController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;

        public TimisoaraAreasController(DbContextTMParking dbContextTMParking)
        { 
          _dbContextTMParking = dbContextTMParking;
        }

        [HttpPost]
        public async Task<IActionResult> PostTimisoaraArea([FromBody] TimisoaraArea newTimisoaraArea)
        {
            if (newTimisoaraArea == null)
            { 
              return BadRequest();
            }

            await _dbContextTMParking.TimisoaraAreas.AddAsync(newTimisoaraArea);
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new { Message = "Area added successfully !" });
            
        }

        [HttpGet]
        public async Task<IActionResult>GetAreas()
        {
            var timisoaraAreas = await _dbContextTMParking.TimisoaraAreas.ToListAsync();
            return Ok(timisoaraAreas);
        }
    }
}
