using Domain.Entities.ProductFolder;
using Service.DTOs.ProductConfigurations;
using Service.DTOs.Variations;

namespace Service.DTOs.VariationOptions;

public class VariationOptionResultDto
{
    public long Id { get; set; }
    public string Value { get; set; }
    public VariationResultDto Variation { get; set; }
}