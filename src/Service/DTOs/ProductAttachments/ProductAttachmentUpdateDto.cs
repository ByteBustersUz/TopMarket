namespace Service.DTOs.ProductAttachments;

public class ProductAttachmentUpdateDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long AttachmentId { get; set; }
}
