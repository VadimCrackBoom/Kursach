namespace AutoServiceApi.Models.DataTransferObjects;

public class AppointmentDto
{
    public int Id { get; set; }
    
    public DateTime AppointmentDateTime { get; set; }
    
    public string Status { get; set; }
    
    public int UserId { get; set; }
}