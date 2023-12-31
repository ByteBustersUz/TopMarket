﻿using Service.DTOs.Regions;

namespace Service.DTOs.Districts;

public class DistrictResultDto
{
    public long Id { get; set; }
    public string NameUz { get; set; }
    public string NameOz { get; set; }
    public string NameRu { get; set; }
    public RegionResultDto Region { get; set; }
}

