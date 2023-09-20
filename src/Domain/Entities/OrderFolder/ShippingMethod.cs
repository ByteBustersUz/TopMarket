using Domain.Commons;

namespace Domain.Entities.OrderFolder;

public class ShippingMethod : Auditable
{
    //Delivery
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}
