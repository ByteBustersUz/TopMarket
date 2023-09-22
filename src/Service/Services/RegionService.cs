﻿using AutoMapper;
using Data.IRepositories;
using Domain.Configuration;
using Domain.Entities.Addresses;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.DTOs.Regions;
using Service.Exceptions;
using Service.Extensions;
using Service.Helpers;
using Service.Interfaces;

namespace Service.Services;

public class RegionService:IRegionService
{
    private readonly IMapper mapper;
    private readonly IRepository<Region> repository;
    public RegionService(IRepository<Region> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<bool> SetAsync()
    {
        var dbSource = this.repository.GetAll();
        if (dbSource.Any())
            throw new AlreadyExistException("Regions are already exist");

        string path = PathHepler.RegionPath;

        var source = File.ReadAllText(path);
        var regions = JsonConvert.DeserializeObject<IEnumerable<RegionCreationDto>>(source);

        foreach (var region in regions)
        {
            var mappedRegion = this.mapper.Map<Region>(region);
            await this.repository.AddAsync(mappedRegion);
            await this.repository.SaveAsync();
        }
        return true;
    }

    public async Task<RegionResultDto> RetrieveByIdAsync(long id)
    {
        var region = await this.repository.GetAsync(r => r.Id.Equals(id), includes: new[] { "Country" })
            ?? throw new NotFoundException("This region is not found");

        var mappedRegion = this.mapper.Map<RegionResultDto>(region);
        return mappedRegion;
    }

    public async Task<IEnumerable<RegionResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var regions = await this.repository.GetAll(includes: new[] { "Country" })
            .ToPaginate(@params)
            .ToListAsync();
        var result = this.mapper.Map<IEnumerable<RegionResultDto>>(regions);
        return result;
    }
}
