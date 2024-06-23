using AutoMapper;
using Data.IRepositories;
using Domain.Entities.ProductFolder;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.VariationOptions;
using Service.DTOs.Variations;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services;

public class VariationService : IVariationService
{
    private readonly IMapper mapper;
    private readonly IRepository<Variation> variationRepository;
    private readonly IRepository<Category> categoryRepository;
    private readonly IProductConfigurationService productConfigurationService;
    public VariationService(
        IMapper mapper, 
        IRepository<Variation> variationRepository,
        IRepository<Category> categoryRepository,
        IProductConfigurationService productConfigurationService)
    {
        this.mapper = mapper;
        this.variationRepository = variationRepository;
        this.categoryRepository = categoryRepository;
        this.productConfigurationService = productConfigurationService;
    }

    public async Task<VariationResultDto> CreateAsync(VariationCreationDto dto, CancellationToken cancellationToken = default)
    {
        var category = await this.categoryRepository.GetAsync(dto.CategoryId, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"This category was not found with {dto.CategoryId}");

        var mappedVariation = this.mapper.Map<Variation>(dto);

        await this.variationRepository.AddAsync(mappedVariation, cancellationToken);
        await this.variationRepository.SaveAsync(cancellationToken);

        return this.mapper.Map<VariationResultDto>(mappedVariation);
    }

    public async Task<VariationResultDto> UpdateAsync(VariationUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var inclusion = new[] { "Category", "VariationOptions" };
        var variation = await this.variationRepository.GetAsync(dto.Id, inclusion, cancellationToken)
            ?? throw new NotFoundException($"There is no variation with id = {dto.Id}");
        ArgumentNullException.ThrowIfNull(variation.Category);

        var mappedVariation = this.mapper.Map(dto, variation);

        this.variationRepository.Update(mappedVariation);
        await this.variationRepository.SaveAsync(cancellationToken);

        return this.mapper.Map<VariationResultDto>(mappedVariation);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var variation = await this.variationRepository.GetAsync(id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException($"There is no variation with id = {id}");

        this.variationRepository.Delete(variation);
        await this.variationRepository.SaveAsync(cancellationToken);

        return true;
    }

    public async Task<VariationResultDto> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var inclusion = new[] { "Category", "VariationOptions" };
        var variation = await this.variationRepository.GetAsync(id, inclusion, cancellationToken)
            ?? throw new NotFoundException($"There is no variation found with id = {id}");

        return this.mapper.Map<VariationResultDto>(variation);
    }

    public async Task<IEnumerable<VariationResultDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var variations = await this.variationRepository.GetAll(includes: new[] { "Category", "VariationOptions" })
            .ToListAsync(cancellationToken);

        return this.mapper.Map<IEnumerable<VariationResultDto>>(variations);
    }

    public async Task<IEnumerable<VariationFeatureResultDto>> GetFeaturesOfProduct(long categoryId, 
        long productItemId,
        CancellationToken cancellationToken = default)
    {
        var variations = await this.variationRepository.GetAll(r=> r.CategoryId.Equals(categoryId))
            .ToListAsync(cancellationToken);

        var resultVariations = this.mapper.Map<List<VariationFeatureResultDto>>(variations);

        var variationOptions = (await productConfigurationService.GetByProductItemIdAsync(productItemId))
            .Select(p=>p.VariationOption)
            .ToList();

        if(variationOptions is not null)
        {
            for(int i=0; i< resultVariations.Count; i++)
            {
                for(int j=0; j< variationOptions.Count; j++)
                {
                    if (resultVariations[i].Id.Equals(variationOptions[j].VariationId))
                    {
                        resultVariations[i].VariationOption = this.mapper.Map<VariationOptionFeatureResult>(variationOptions[j]);
                    }
                }
            }
        }

        return resultVariations.AsEnumerable();
    }
}
