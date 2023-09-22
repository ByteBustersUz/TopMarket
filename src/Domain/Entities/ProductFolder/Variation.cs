using Domain.Commons;

namespace Domain.Entities.ProductFolder;

public class Variation : Auditable
{
    public string Name { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public ICollection<VariationOption> VariationOptions { get; set; }
}
