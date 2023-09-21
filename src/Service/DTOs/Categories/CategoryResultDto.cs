using Domain.Entities.ProductFolder;
using Service.DTOs.Products;
using Service.DTOs.Variations;

namespace Service.DTOs.Categories;

public class CategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long  ParentId { get; set; }
    public ICollection<ProductResultDto> Products { get; set; }
    public ICollection<VariationResultDto> Variations { get; set; }
}