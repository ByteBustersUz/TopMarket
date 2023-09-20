using Domain.Commons;

namespace Domain.Entities.ProductFolder;

public class VarationOption : Auditable
{
    public long VariationId { get; set; }
    public Variation Variation { get; set; } = default!;

    public double Value { get; set; }
}
