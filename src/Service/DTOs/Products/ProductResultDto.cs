using Service.DTOs.Categories;

namespace Service.DTOs.Products;

public class ProductResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryResultDto Category { get; set; }

    //public AttachmentResultDto? Attachment { get; set; }
}