using System.ComponentModel.DataAnnotations;

namespace TMParking_Backend.Models
{
    public class FileInterface
    {
        [Key]
        public int FileId { get; set; }

        public string FileName { get; set; }    

        public string FileDataAsBase64 { get; set; }

        public string FilePath { get; set; }
    }
}
