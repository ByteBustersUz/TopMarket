using AutoMapper;
using Data.IRepositories;
using Domain.Entities.ProductFolder;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Variations;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services;

public class VariationService : IVariationService
{
    private readonly IMapper mapper;
    private readonly IRepository<Variation> repository;
    private readonly IRepository<Category> categoryRepository;
    public VariationService(
        IMapper mapper, 
        IRepository<Variation> repository,
        IRepository<Category> categoryRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.categoryRepository = categoryRepository;
    }

    public async Task<VariationResultDto> CreateAsync(VariationCreationDto dto)
    {
        var existCategory = await this.categoryRepository.GetAsync(c => c.Id.Equals(dto.CategoryId))
            ?? throw new NotFoundException($"This category was not found with {dto.CategoryId}");

        var mappedVariation = this.mapper.Map<Variation>(dto);

        await this.repository.AddAsync(mappedVariation);
        await this.repository.SaveAsync();

        return this.mapper.Map<VariationResultDto>(mappedVariation);
    }

    public async Task<VariationResultDto> UpdateAsync(VariationUpdateDto dto)
    {
        var existVariation = await this.repository.GetAsync(c => c.Id.Equals(dto.Id), includes: new[] { "Category", "VariationOptions" })
            ?? throw new NotFoundException($"This variation was not found with {dto.Id}");

        var existCategory = await this.categoryRepository.GetAsync(c => c.Id.Equals(dto.CategoryId))
            ?? throw new NotFoundException($"This category was not found with {dto.CategoryId}");

        var mappedVariation = this.mapper.Map(dto, existVariation);

        this.repository.Update(mappedVariation);
        await this.repository.SaveAsync();

        return this.mapper.Map<VariationResultDto>(mappedVariation);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existVariation = await this.repository.GetAsync(c => c.Id.Equals(id))
            ?? throw new NotFoundException($"This variation was not found with {id}");

        this.repository.Delete(existVariation);
        await this.repository.SaveAsync();

        return true;
    }

    public async Task<VariationResultDto> GetByIdAsync(long id)
    {
        var existVariation = await this.repository.GetAsync(c => c.Id.Equals(id), includes: new[] { "Category", "VariationOptions" })
            ?? throw new NotFoundException($"This variation was not found with {id}");

        return this.mapper.Map<VariationResultDto>(existVariation);
    }

    public async Task<IEnumerable<VariationResultDto>> GetAllAsync()
    {
        var variations = await this.repository.GetAll(includes: new[] { "Category", "VariationOptions" }).ToListAsync();

        return this.mapper.Map<IEnumerable<VariationResultDto>>(variations);
    }
}
