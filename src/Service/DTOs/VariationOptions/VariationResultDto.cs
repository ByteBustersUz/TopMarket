namespace Service.DTOs.VariationOptions;

public class VariationResultDto
{
    public long Id { get; set; }
    public string Value { get; set; }
    public VariationResultDto Variation { get; set; }
}