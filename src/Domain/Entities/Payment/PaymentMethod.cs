using Domain.Commons;
using Domain.Entities.UserFolder;

namespace Domain.Entities.Payment;

public class PaymentMethod : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public long PaymentTypeId { get; set; }
    public PaymentType PaymentType { get; set; } = default!;

    public string Provider {  get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public bool IsDeafult { get; set; }
}
