using Domain.Commons;

namespace Domain.Entities.Address;

public class Country : Auditable
{
    public string Name { get; set; }
    public string CountryCode { get; set; }
}
