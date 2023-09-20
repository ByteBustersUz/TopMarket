using Domain.Commons;
using System.Globalization;

namespace Domain.Entities.ProductFolder;

public class Promotion : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal DiscountRate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
