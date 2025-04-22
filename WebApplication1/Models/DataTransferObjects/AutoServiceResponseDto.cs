namespace AutoServiceApi.Models.DataTransferObjects;

public class AutoServiceResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string WorkingHours { get; set; } // Форматированное время
}