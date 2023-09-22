using Domain.Commons;

namespace Domain.Entities.Addresses;

public class Country : Auditable
{
    public string Name { get; set; }
    public string CountryCode { get; set; }

    public ICollection<Address> Addresses { get; set; }
    public ICollection<Region> Regions { get; set; }
}
