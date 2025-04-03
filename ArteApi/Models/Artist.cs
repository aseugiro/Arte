using System.ComponentModel.DataAnnotations;

namespace ArteApi.Models
{
    public class Artist
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

    }
}
