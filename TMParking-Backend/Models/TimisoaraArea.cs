using System.ComponentModel.DataAnnotations;

namespace TMParking_Backend.Models
{
    public class TimisoaraArea
    {
        [Key]
        public int IdTimisoaraArea { get; set; }
        public string Name { get; set; }
    }
}
