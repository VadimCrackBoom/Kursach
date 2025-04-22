namespace AutoServiceApi.Models.DataTransferObjects;

public class UpdateServiceOfferDto: ServiceOfferBaseDto
{
    public bool UpdateTimestamp { get; set; }
    public int Id { get; set; }
}