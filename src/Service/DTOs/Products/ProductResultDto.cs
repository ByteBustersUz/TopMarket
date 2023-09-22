using Service.DTOs.Categories;
using Service.DTOs.ProductAttachments;
using Service.DTOs.ProductItems;

namespace Service.DTOs.Products;

public class ProductResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryResultDto Category { get; set; }
    public ICollection<ProductItemResultDto> ProductItems { get; set; }
    public ICollection<ProductAttachmentResultDto> ProductAttachments { get; set; }
}