using Domain.Commons;

namespace Domain.Entities.Payment;

public class PaymentType : Auditable
{
    public string Value { get; set; }
}
