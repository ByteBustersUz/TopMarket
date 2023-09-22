﻿using AutoMapper;
using Data.IRepositories;
using Domain.Configuration;
using Domain.Entities.Addresses;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Addresses;
using Service.Exceptions;
using Service.Extensions;
using Service.Interfaces;

namespace Service.Services;

public class AddressService:IAddressService
{
    private readonly IMapper mapper;
    private readonly IRepository<Region> regionRepository;
    private readonly IRepository<Address> addressRepository;
    private readonly IRepository<Country> countryRepository;
    private readonly IRepository<District> districtRepository;
    public AddressService(
        IMapper mapper,
        IRepository<Region> regionRepository,
        IRepository<Address> addressRepository,
        IRepository<Country> countryRepository,
        IRepository<District> districtRepository)
    {
        this.mapper = mapper;
        this.regionRepository = regionRepository;
        this.addressRepository = addressRepository;
        this.countryRepository = countryRepository;
        this.districtRepository = districtRepository;
    }

    public async Task<AddressResultDto> CreateAsync(AddressCreationDto dto)
    {
        var existRegion = await this.regionRepository.GetAsync(r => r.Id.Equals(dto.RegionId))
            ?? throw new NotFoundException($"This regionId was not found with {dto.RegionId}");

        var existCountry = await this.countryRepository.GetAsync(r => r.Id.Equals(dto.CountryId))
            ?? throw new NotFoundException($"This countryId was not found with {dto.CountryId}");

        var existDistrict = await this.districtRepository.GetAsync(r => r.Id.Equals(dto.DistrictId))
            ?? throw new NotFoundException($"This districtId was not found with {dto.DistrictId}");

        var mappedAddress = this.mapper.Map<Address>(dto);
        await this.addressRepository.AddAsync(mappedAddress);
        await this.addressRepository.SaveAsync();

        mappedAddress.Region = existRegion;
        mappedAddress.Country = existCountry;
        mappedAddress.District = existDistrict;

        return this.mapper.Map<AddressResultDto>(mappedAddress);
    }

    public async Task<AddressResultDto> ModifyAsync(AddressUpdateDto dto)
    {
        var existAddress = await this.addressRepository.GetAsync(r => r.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This id was not found with {dto.Id}");

        var existRegion = await this.regionRepository.GetAsync(r => r.Id.Equals(dto.RegionId))
            ?? throw new NotFoundException($"This regionId was not found with {dto.RegionId}");

        var existCountry = await this.countryRepository.GetAsync(r => r.Id.Equals(dto.CountryId))
            ?? throw new NotFoundException($"This countryId was not found with {dto.CountryId}");

        var existDistrict = await this.districtRepository.GetAsync(r => r.Id.Equals(dto.DistrictId))
            ?? throw new NotFoundException($"This districtId was not found with {dto.DistrictId}");

        existAddress.RegionId = existRegion.Id;
        existAddress.CountryId = existCountry.Id;
        existAddress.DistrictId = existDistrict.Id;
       
        this.mapper.Map(dto, existAddress);
        this.addressRepository.Update(existAddress);
        await this.addressRepository.SaveAsync();

        existAddress.Region = existRegion;
        existAddress.Country = existCountry;
        existAddress.District = existDistrict;

        return mapper.Map<AddressResultDto>(existAddress);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var existAddress = await this.addressRepository.GetAsync(r => r.Id.Equals(id))
            ?? throw new NotFoundException($"This id was not found with {id}");

        this.addressRepository.Delete(existAddress);
        await this.addressRepository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<AddressResultDto>> RetrieveAllAsync()
    {
        var addresses = await this.addressRepository.GetAll(includes: new[] { "Country", "Region", "District" })
            .ToListAsync();

        return this.mapper.Map<IEnumerable<AddressResultDto>>(addresses);
    }

    public async Task<IEnumerable<AddressResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var addresses = await this.addressRepository.GetAll(includes: new[] { "Country", "Region", "District" })
            .ToPaginate(@params)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<AddressResultDto>>(addresses);
    }

    public async Task<AddressResultDto> RetrieveByIdAsync(long id)
    {
        var existAddress = await this.addressRepository.GetAsync(p => p.Id.Equals(id),
            includes: new[] { "Country", "Region", "District" })
            ?? throw new NotFoundException($"This id was not found with {id}");

        return this.mapper.Map<AddressResultDto>(existAddress);
    }
}
