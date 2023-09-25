namespace Service.DTOs.ProductItemAttachments;

public class ProductItemAttachmentUpdateDto
{
    public long Id { get; set; }
    public long ProductItemId { get; set; }
    public long AttachmentId { get; set; }
}
