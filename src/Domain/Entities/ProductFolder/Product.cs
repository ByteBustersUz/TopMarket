using Domain.Commons;
using Domain.Entities.AttachmentFolder;

namespace Domain.Entities.ProductFolder;

public class Product : Auditable
{
    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public long? AttachmentId { get; set; }
    public Attachment Attachment { get; set; } = default!;
}
