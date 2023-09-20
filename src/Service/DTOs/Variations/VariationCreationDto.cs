using Domain.Entities.ProductFolder;

namespace Service.DTOs.Variations;

public class VariationCreationDto
{
    public string Name { get; set; }
    public long CategoryId { get; set; }
}
