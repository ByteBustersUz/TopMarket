using Service.DTOs.Categories;
using Service.DTOs.Promotions;

namespace Service.DTOs.PromotionCategories;

public class PromotionCategoryResultDto
{
    public long Id { get; set; }
    public CategoryResultDto Category { get; set; }
    public PromotionResultDto Promotion { get; set; }
}