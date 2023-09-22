using Service.DTOs.Categories;
using Service.DTOs.VariationOptions;

namespace Service.DTOs.Variations;

public class VariationResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CategoryResultDto Category{ get; set; }
    public ICollection<VariationOptionResultDto> VariationOptions { get; set; }
}