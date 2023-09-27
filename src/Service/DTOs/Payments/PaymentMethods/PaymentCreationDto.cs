using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Payments.PaymentMethods;

public class PaymentCreationDto
{
    public long UserId { get; set; }
    public long PaymentTypeId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public bool IsDeafult { get; set; }
}
