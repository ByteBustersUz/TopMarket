using Domain.Commons;
using Domain.Entities.AttachmentFolder;

namespace Domain.Entities.ProductFolder;

public class ProductItem : Auditable
{
    public long ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public string SKU { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }
}
