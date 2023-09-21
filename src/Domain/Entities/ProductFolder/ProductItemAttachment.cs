using Domain.Commons;
using Domain.Entities.AttachmentFolder;

namespace Domain.Entities.ProductFolder;

public class ProductItemAttachment : Auditable
{
    public long ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; }
    public long AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}
