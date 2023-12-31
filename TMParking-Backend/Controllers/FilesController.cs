using Microsoft.AspNetCore.Mvc;
using System.IO.Enumeration;
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

                // Numele directorului pe care vrei să îl creezi
                string directoryName = "NumeDirector";

                // Obține calea către directorul curent
                string currentDirectory = Directory.GetCurrentDirectory();

                // Concatenează numele directorului nou la calea directorului curent
                string directoryPath = Path.Combine(currentDirectory, directoryName);

                // Verifică dacă directorul există; dacă nu, îl creează
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Calea către fișier
                string filePath = Path.Combine(directoryPath, file.FileName);

                // Salvează fișierul pe disc
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Convertește conținutul fișierului într-un șir Base64
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(fileBytes);

                    var fileEntity = new FileInterface
                    {
                        FileName = file.FileName,
                        FileDataAsBase64 = base64String,
                        FilePath = filePath // Salvăm calea către fișier
                    };

                    // Salvare în baza de date
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

                // Încarcă fișierul de pe disc
                var fileStream = new FileStream(file.FilePath, FileMode.Open, FileAccess.Read);

                // Convertește conținutul fișierului într-un șir Base64
                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(fileBytes);

                    // Returnează conținutul fișierului ca un obiect JSON cu șirul Base64
                    return Ok(new { fileData = base64String });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
