using Domain.Commons;

namespace Domain.Entities.ProductFolder;

public class VariationOption : Auditable
{
    public string Value { get; set; }
    public long VariationId { get; set; }
    public Variation Variation { get; set; } = default!;
    public ICollection<ProductConfiguration> ProductConfigurations { get; set; }
}
