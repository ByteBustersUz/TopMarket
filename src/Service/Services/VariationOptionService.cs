using AutoMapper;
using Data.IRepositories;
using Domain.Entities.ProductFolder;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.VariationOptions;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services;

public class VariationOptionService : IVariationOptionService
{
    private readonly IMapper mapper;
    private readonly IRepository<VariationOption> repository;
    private readonly IRepository<Variation> variationRepository;
    public VariationOptionService(
        IMapper mapper,
        IRepository<VariationOption> repository,
        IRepository<Variation> variationRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.variationRepository = variationRepository;
    }

    public async Task<VariationOptionResultDto> CreateAsync(VariationOptionCreationDto dto)
    {
        var existVariation = await this.variationRepository.GetAsync(c => c.Id.Equals(dto.VariationId))
            ?? throw new NotFoundException($"This variation was not found with {dto.VariationId}");

        var mappedVariationOption = this.mapper.Map<VariationOption>(dto);

        await this.repository.AddAsync(mappedVariationOption);
        await this.repository.SaveAsync();

        return this.mapper.Map<VariationOptionResultDto>(mappedVariationOption);
    }

    public async Task<VariationOptionResultDto> UpdateAsync(VariationOptionUpdateDto dto)
    {
        var existVariationOption = await this.repository.GetAsync(c => c.Id.Equals(dto.Id), includes: new[] { "Variation", "ProductConfigurations" })
            ?? throw new NotFoundException($"This variationOption was not found with {dto.Id}");

        var existVariation = await this.variationRepository.GetAsync(c => c.Id.Equals(dto.VariationId))
            ?? throw new NotFoundException($"This variation was not found with {dto.VariationId}");

        var mappedVariationOption = this.mapper.Map(dto, existVariationOption);

        this.repository.Update(mappedVariationOption);
        await this.repository.SaveAsync();

        return this.mapper.Map<VariationOptionResultDto>(mappedVariationOption);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existVariationOption = await this.repository.GetAsync(c => c.Id.Equals(id))
            ?? throw new NotFoundException($"This variationOption was not found with {id}");

        this.repository.Delete(existVariationOption);
        await this.repository.SaveAsync();

        return true;
    }

    public async Task<VariationOptionResultDto> GetByIdAsync(long id)
    {
        var existVariationOption = await this.repository.GetAsync(c => c.Id.Equals(id), includes: new[] { "Variation", "ProductConfigurations" })
            ?? throw new NotFoundException($"This variationOption was not found with {id}");

        return this.mapper.Map<VariationOptionResultDto>(existVariationOption);
    }

    public async Task<IEnumerable<VariationOptionResultDto>> GetAllAsync()
    {
        var variationOptions = await this.repository.GetAll(includes: new[] { "Variation", "ProductConfigurations" }).ToListAsync();

        return this.mapper.Map<IEnumerable<VariationOptionResultDto>>(variationOptions);
    }
}
