using Service.DTOs.Categories;

namespace Service.DTOs.Variations;

public class VariationResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CategoryResultDto Category{ get; set; }
}