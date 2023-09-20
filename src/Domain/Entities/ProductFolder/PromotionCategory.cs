using Domain.Commons;

namespace Domain.Entities.ProductFolder;

public class PromotionCategory : Auditable
{
    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public long PromotionId { get; set; }
    public Promotion Promotion { get; set; } = default!;
}
