using Domain.Commons;
using Domain.Entities.ProductFolder;

namespace Domain.Entities.Shopping;

public class ShoppingCartItem : Auditable
{
    public long CartId { get; set; }
    public ShoppingCart Cart { get; set; } = default!;

    public long ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; } = default!;

    public long Quantity { get; set; }
}
