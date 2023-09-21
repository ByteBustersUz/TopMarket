using Domain.Entities.ProductFolder;

namespace Service.DTOs.ProductAttachments;

public class ProductAttachmentCreationDto
{
    public long ProductId { get; set; }
    public long AttachmentId { get; set; }
}
