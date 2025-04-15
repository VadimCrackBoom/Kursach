using System.ComponentModel.DataAnnotations;
using AutoServiceApi.Models;

namespace WebApplication1.AutoServiceApi.Models
{
    public class ServiceOffer
    {
        public int Id { get; set; }
    
        [Microsoft.Build.Framework.Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    
        [StringLength(500)]
        public string? Description { get; set; }
    
        [Range(0, 100000)]
        public decimal Price { get; set; }
    
        [Microsoft.Build.Framework.Required]
        public string Category { get; set; } = "Основные"; // Новое поле
    
        public int DurationMinutes { get; set; } = 60; // Новое поле
    
        public bool IsActive { get; set; } = true; // Новое поле
    
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Новое поле
    
        public DateTime? LastUpdated { get; set; } // Новое поле
    
        // Связи
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
