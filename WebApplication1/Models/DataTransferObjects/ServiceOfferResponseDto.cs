namespace AutoServiceApi.Models.DataTransferObjects;

public class ServiceOfferResponseDto : ServiceOfferBaseDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdated { get; set; }
    
    
    public string DurationFormatted => $"{DurationMinutes / 60} ч {DurationMinutes % 60} мин";
}