using AutoMapper;
using Data.IRepositories;
using Domain.Entities.ProductFolder;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Attachments;
using Service.DTOs.ProductItems;
using Service.DTOs.Products;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services;

public class ProductItemService : IProductItemService
{
    private readonly IMapper mapper;
    private readonly IRepository<ProductItem> repository;
    private readonly IRepository<Product> productRepository;
    public ProductItemService(
        IMapper mapper,
        IRepository<ProductItem> repository,
        IRepository<Product> productRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.productRepository = productRepository;
    }

    public async Task<ProductItemResultDto> CreateAsync(ProductItemCreationDto dto)
    {
        var existProduct = await this.productRepository.GetAsync(c => c.Id.Equals(dto.ProductId))
            ?? throw new NotFoundException($"This product was not found with {dto.ProductId}");

        var mappedProductItem = this.mapper.Map<ProductItem>(dto);

        await this.repository.AddAsync(mappedProductItem);
        await this.repository.SaveAsync();

        return this.mapper.Map<ProductItemResultDto>(mappedProductItem);
    }

    public async Task<ProductItemResultDto> UpdateAsync(ProductItemUpdateDto dto)
    {
        var existProductItem = await this.repository.GetAsync(c => c.Id.Equals(dto.Id), includes: new[] { "Product","OrderLines", "ProductConfigurations", "ProductItemAttachments", "ShoppingCartItems" })
            ?? throw new NotFoundException($"This productItem was not found with {dto.Id}");

        var existProduct = await this.productRepository.GetAsync(c => c.Id.Equals(dto.ProductId))
            ?? throw new NotFoundException($"This product was not found with {dto.ProductId}");

        var mappedProductItem = this.mapper.Map(dto, existProductItem);

        this.repository.Update(mappedProductItem);
        await this.repository.SaveAsync();

        return this.mapper.Map<ProductItemResultDto>(mappedProductItem);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existProductItem = await this.repository.GetAsync(c => c.Id.Equals(id))
            ?? throw new NotFoundException($"This productItem was not found with {id}");

        this.repository.Delete(existProductItem);
        await this.repository.SaveAsync();

        return true;
    }

    public async Task<ProductItemResultDto> GetByIdAsync(long id)
    {
        var existProductItem = await this.repository.GetAsync(c => c.Id.Equals(id), includes: new[] { "Product","OrderLines", "ProductConfigurations", "ProductItemAttachments", "ShoppingCartItems" })
            ?? throw new NotFoundException($"This productItem was not found with {id}");

        return this.mapper.Map<ProductItemResultDto>(existProductItem);
    }

    public async Task<IEnumerable<ProductItemResultDto>> GetAllAsync()
    {
        var ProductItems = await this.repository.GetAll(includes: new[] { "Product","OrderLines", "ProductConfigurations", "ProductItemAttachments", "ShoppingCartItems" }).ToListAsync();

        return this.mapper.Map<IEnumerable<ProductItemResultDto>>(ProductItems);
    }

    public Task<ProductResultDto> ImageUploadAsync(long productId, AttachmentCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResultDto> ImageUpdateAsync(long productId, AttachmentCreationDto dto)
    {
        throw new NotImplementedException();
    }
}
