using Domain.Commons;

namespace Domain.Entities.Addresses;

public class Region : Auditable
{
    public string NameUz { get; set; }
    public string NameOz { get; set; }
    public string NameRu { get; set; }
    public long CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public ICollection<District> Districts { get; set; }
}
