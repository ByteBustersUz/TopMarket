using Domain.Commons;
using Domain.Entities.AttachmentFolder;

namespace Domain.Entities.ProductFolder;

public class ProductAttachment : Auditable
{
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}
