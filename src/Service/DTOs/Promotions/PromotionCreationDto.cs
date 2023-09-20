namespace Service.DTOs.Promotions;

public class PromotionCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal DiscountRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
