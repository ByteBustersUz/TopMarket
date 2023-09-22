using Domain.Commons;

namespace Domain.Entities.Addresses;

public class District : Auditable
{
    public string NameUz { get; set; }
    public string NameOz { get; set; }
    public string NameRu { get; set; }
    public long RegionId { get; set; }
    public Region Region { get; set; }
    public ICollection<Address> Addresses { get; set; }
}
