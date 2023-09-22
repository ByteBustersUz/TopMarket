using AutoMapper;
using Data.IRepositories;
using Domain.Entities.ProductFolder;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Categories;
using Service.DTOs.Promotions;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services;

public class PromotionService : IPromotionService
{
    private readonly IMapper mapper;
    private readonly IRepository<Promotion> repository;
    public PromotionService(IMapper mapper, IRepository<Promotion> repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<PromotionResultDto> CreateAsync(PromotionCreationDto dto)
    {
        var existPromotion = await this.repository.GetAsync(c => c.Name.ToLower().Equals(dto.Name.ToLower()) && c.EndDate>=DateTime.UtcNow);
        if (existPromotion is not null)
            throw new AlreadyExistException($"This promotion already exist with {dto.Name}");

        var mappedPromotion = this.mapper.Map<Promotion>(dto);

        await this.repository.AddAsync(mappedPromotion);
        await this.repository.SaveAsync();

        return this.mapper.Map<PromotionResultDto>(mappedPromotion);
    }

    public async Task<PromotionResultDto> UpdateAsync(PromotionUpdateDto dto)
    {
        var existPromotion = await this.repository.GetAsync(c => c.Id.Equals(dto.Id), includes: new[] { "PromotionCategories" })
            ?? throw new NotFoundException($"This promotion was not found with {dto.Id}");

        if (!existPromotion.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase))
        {
            var existPromotion2 = await this.repository.GetAsync(c => c.Name.ToLower().Equals(dto.Name.ToLower())&&c.EndDate>=DateTime.UtcNow);
            if (existPromotion2 is not null)
                throw new AlreadyExistException($"This promotion already exist with {dto.Name}");
        }

        var mappedPromotion = this.mapper.Map(dto, existPromotion);

        this.repository.Update(mappedPromotion);
        await this.repository.SaveAsync();

        return this.mapper.Map<PromotionResultDto>(mappedPromotion);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existPromotion = await this.repository.GetAsync(c => c.Id.Equals(id))
            ?? throw new NotFoundException($"This promotion was not found with {id}");

        this.repository.Delete(existPromotion);
        await this.repository.SaveAsync();

        return true;
    }

    public async Task<PromotionResultDto> GetByIdAsync(long id)
    {
        var existPromotion = await this.repository.GetAsync(c => c.Id.Equals(id), includes: new[] { "PromotionCategories" })
            ?? throw new NotFoundException($"This promotion was not found with {id}");

        return this.mapper.Map<PromotionResultDto>(existPromotion);
    }

    public async Task<IEnumerable<PromotionResultDto>> GetAllAsync()
    {
        var promotions = await this.repository.GetAll(includes: new[] { "PromotionCategories" }).ToListAsync();

        return this.mapper.Map<IEnumerable<PromotionResultDto>>(promotions);
    }
}
