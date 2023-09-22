﻿using Domain.Commons;
using Domain.Entities.OrderFolder;
using Domain.Entities.UserFolder;

namespace Domain.Entities.Addresses;

public class Address : Auditable
{
    public string Street { get; set; }
    public string Floor { get; set; }
    public string Home { get; set; }
    public string DoorCode { get; set; }

    public long CountryId { get; set; }
    public Country Country { get; set; }

    public long RegionId { get; set; }
    public Region Region { get; set; }

    public long DistrictId { get; set; }
    public District District { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<UserAddress> UserAddresses { get; set; }
}
