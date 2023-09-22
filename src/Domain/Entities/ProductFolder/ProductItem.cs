using Domain.Commons;
using Domain.Entities.AttachmentFolder;
using Domain.Entities.OrderFolder;
using Domain.Entities.Shopping;

namespace Domain.Entities.ProductFolder;

public class ProductItem : Auditable
{
    public long ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public string SKU { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; }
    public ICollection<ProductConfiguration> ProductConfigurations { get; set; } 
    public ICollection<ProductItemAttachment> ProductItemAttachments { get; set; }
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
}
