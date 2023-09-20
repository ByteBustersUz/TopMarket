using Domain.Entities.ProductFolder;

namespace Service.DTOs.ProductConfigurations;

public class ProductConfigurationResultDto
{ 
    public long Id { get; set; }
    public ProductItem ProductItem { get; set; }
    public VariationOption Variation { get; set; }
}