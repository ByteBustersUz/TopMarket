using Domain.Entities.ProductFolder;

namespace Service.DTOs.PromotionCategories;

public class PromotionCategoryCreationDto
{
    public long CategoryId { get; set; }
    public long PromotionId { get; set; }
}
