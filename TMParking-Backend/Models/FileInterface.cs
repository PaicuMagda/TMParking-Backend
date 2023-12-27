using System.ComponentModel.DataAnnotations;

namespace TMParking_Backend.Models
{
    public class FileInterface
    {
        [Key]
        public int FileId { get; set; }

        public string FileName { get; set; }    

        public byte[] FileData { get; set; }
    }
}
