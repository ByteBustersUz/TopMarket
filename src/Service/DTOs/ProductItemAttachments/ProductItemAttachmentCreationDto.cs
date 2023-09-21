using Domain.Entities.ProductFolder;

namespace Service.DTOs.ProductItemAttachments;

public class ProductItemAttachmentCreationDto
{
    public long ProductItemId { get; set; }
    public long AttachmentId { get; set; }
}
