using System.ComponentModel.DataAnnotations;

namespace WebAppArte.Models
{
    public class Paint
    {

        public int ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
