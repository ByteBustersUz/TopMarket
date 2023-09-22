namespace Service.DTOs.VariationOptions;

public class VariationOptionResultDto
{
    public long Id { get; set; }
    public string Value { get; set; }
    public VariationOptionResultDto Variation { get; set; }
}