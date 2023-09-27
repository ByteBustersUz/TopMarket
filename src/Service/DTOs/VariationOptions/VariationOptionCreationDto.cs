using Domain.Entities.ProductFolder;

namespace Service.DTOs.VariationOptions;

public class VariationOptionCreationDto
{
    public string Value { get; set; }
    public long VariationId { get; set; }
    public long ProductItemId { get; set; }
}
