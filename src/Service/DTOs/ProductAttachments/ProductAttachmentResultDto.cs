using Service.DTOs.Attachments;
using Service.DTOs.Products;

namespace Service.DTOs.ProductAttachments;

public class ProductAttachmentResultDto
{ 
    public long Id { get; set; }
    public AttachmentResultDto Attachment  { get; set; }
}