using Domain.Commons;
using Domain.Entities.AttachmentFolder;

namespace Domain.Entities.ProductFolder;

public class ProductItem : Auditable
{
    public long ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public decimal SKU { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }

    public long? AttachmentId { get; set; }
    public Attachment Attachment { get; set; } = default!;

}
