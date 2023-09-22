using Service.DTOs.PromotionCategories;

namespace Service.DTOs.Promotions;

public class PromotionResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal DiscountRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<PromotionCategoryResultDto> PromotionCategories { get; set; }
}