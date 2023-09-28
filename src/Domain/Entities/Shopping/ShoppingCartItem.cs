using Domain.Commons;
using Domain.Entities.ProductFolder;

namespace Domain.Entities.Shopping;

public class ShoppingCartItem : Auditable
{
    public double Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Summ { get; set; }
    public long CartId { get; set; }
    public ShoppingCart Cart { get; set; }

    public long ProductId { get; set; }
    public ProductItem Product { get; set; }

}
