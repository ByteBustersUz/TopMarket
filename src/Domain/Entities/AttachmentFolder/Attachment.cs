using Domain.Commons;
using Domain.Entities.ProductFolder;

namespace Domain.Entities.AttachmentFolder;

public class Attachment : Auditable
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public ICollection<ProductAttachment> ProductAttachments { get; set; }
    public ICollection<ProductItemAttachment> ProductItemAttachments { get; set; }
}
