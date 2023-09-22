using Domain.Commons;

namespace Domain.Entities.OrderFolder;

public class OrderStatus : Auditable
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Order> Orders { get; set; }
}
