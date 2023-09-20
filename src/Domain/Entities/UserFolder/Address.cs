using Domain.Commons;

namespace Domain.Entities.UserFolder;

public class Address : Auditable
{
    public string? HomeNumber { get; set; }
    public string? Floor { get; set; }
    public string StreetName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;

    public long CountryId { get; set; }
    public Country Country { get; set; } = default!;
}
