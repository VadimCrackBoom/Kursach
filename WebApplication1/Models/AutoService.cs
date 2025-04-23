using System.ComponentModel.DataAnnotations;

namespace WebApplication1.AutoServiceApi.Models
{
    public class AutoService
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;
    }
}
