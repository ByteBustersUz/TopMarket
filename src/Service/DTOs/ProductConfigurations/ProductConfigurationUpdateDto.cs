namespace Service.DTOs.ProductConfigurations;

public class ProductConfigurationUpdateDto
{
    public long Id { get; set; }
    public long ProductItemId { get; set; }
    public long VariationOptionId { get; set; }
}
