using Domain.Commons;
using Domain.Entities.Addresses;
using Domain.Entities.Payment;
using Domain.Entities.UserFolder;

namespace Domain.Entities.OrderFolder;

public class Order : Auditable
{
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    
    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public long PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = default!;

    public long AddressId { get; set; }
    public Address Address { get; set; } = default!;

    public long ShippingMethodId {  get; set; }
    public ShippingMethod ShippingMethod { get; set; } = default!;

    public long StatusId { get; set; }
    public OrderStatus Status { get; set; } = default!;
}
