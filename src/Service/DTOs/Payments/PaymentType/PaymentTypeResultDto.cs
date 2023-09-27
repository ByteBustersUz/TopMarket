using Domain.Entities.Payment;
using Service.DTOs.PaymentType;

namespace Service.DTOs.Payments.PaymentType;

public class PaymentTypeResultDto
{
    public long Id { get; set; }
    public string Value { get; set; }
    public ICollection<PaymentResultDto> PaymentMethods { get; set; }
}


