using Service.DTOs.Attachments;
using Service.DTOs.ProductItems;

namespace Service.DTOs.ProductItemAttachments;

public class ProductItemAttachmentResultDto
{
    public long Id { get; set; }
    public AttachmentResultDto Attachment { get; set; }
}