using Domain.Entities.Addresses;

namespace Domain.Entities.UserFolder;

public class UserAddress
{
    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public long AddressId { get; set; }
    public Address Address { get; set; } = default!;

    public bool IsDefault { get; set; }
}
