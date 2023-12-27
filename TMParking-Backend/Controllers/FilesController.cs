
using Microsoft.AspNetCore.Mvc;
using TMParking_Backend.Data;
using TMParking_Backend.Models;

namespace TMParking_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;

        public FilesController(DbContextTMParking dbContextTMParking)
        {
            _dbContextTMParking = dbContextTMParking;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Invalid file");

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    var fileEntity = new FileInterface
                    {
                        FileName = file.FileName,
                        FileData = memoryStream.ToArray()
                    };

                    _dbContextTMParking.Files.Add(fileEntity);
                    await _dbContextTMParking.SaveChangesAsync();
                }

                return Ok("File uploaded successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            try
            {
                var file = await _dbContextTMParking.Files.FindAsync(id);

                if (file == null)
                    return NotFound("File not found");

                return File(file.FileData, "application/octet-stream", file.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
