using AutoMapper;
using Data.IRepositories;
using Data.Repositories;
using Domain.Entities.AttachmentFolder;
using Domain.Entities.ProductFolder;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Attachments;
using Service.DTOs.ProductAttachments;
using Service.DTOs.ProductItemAttachments;
using Service.DTOs.ProductItems;
using Service.DTOs.Products;
using Service.Exceptions;
using Service.Helpers;
using Service.Interfaces;

namespace Service.Services;

public class ProductItemService : IProductItemService
{
    private readonly IMapper mapper;
    private readonly IRepository<ProductItem> repository;
    private readonly IVariationService variationService;
    private readonly IAttachmentService attachmentService;
    private readonly IRepository<Product> productRepository;
    private readonly IProductItemAttachmentService productItemAttachmentService;
    public ProductItemService(
        IMapper mapper,
        IRepository<ProductItem> repository,
        IVariationService variationService,
        IAttachmentService attachmentService,
        IRepository<Product> productRepository,
        IProductItemAttachmentService productItemAttachmentService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.variationService = variationService;
        this.productRepository = productRepository;
        this.attachmentService = attachmentService;
        this.productItemAttachmentService = productItemAttachmentService;
    }

    public async Task<ProductItemResultDto> CreateAsync(ProductItemCreationDto dto)
    {
        var existProduct = await this.productRepository.GetAsync(c => c.Id.Equals(dto.ProductId))
            ?? throw new NotFoundException($"This product was not found with {dto.ProductId}");

        var mappedProductItem = this.mapper.Map<ProductItem>(dto);
        mappedProductItem.SKU = SKUHelper.GenerateSKU();
        mappedProductItem.QuantityInStock = 0;

        await this.repository.AddAsync(mappedProductItem);
        await this.repository.SaveAsync();

        mappedProductItem.Product = existProduct;

        return this.mapper.Map<ProductItemResultDto>(mappedProductItem);
    }

    public async Task<ProductItemResultDto> AddAsync(ProductItemAdditionDto dto)
    {
        var existProductItem = await this.repository.GetAsync(c => c.Id.Equals(dto.Id), new string[] { "Product" })
            ?? throw new NotFoundException($"This product was not found with {dto.Id}");

        var mappedProductItem = this.mapper.Map(dto, existProductItem);

        this.repository.Update(mappedProductItem);
        await this.repository.SaveAsync();

        return this.mapper.Map<ProductItemResultDto>(mappedProductItem);
    }

    public async Task<ProductItemResultDto> UpdateAsync(ProductItemUpdateDto dto)
    {
        var existProductItem = await this.repository.GetAsync(c => c.Id.Equals(dto.Id), includes: new[] { "Product", "ProductItemAttachments.Attachment" })
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
        var existProductItem = await this.repository.GetAsync(c => c.Id.Equals(id), includes: new[] { "Product", "ProductItemAttachments.Attachment" })
            ?? throw new NotFoundException($"This productItem was not found with {id}");

        var categoryId = existProductItem.Product.CategoryId;

        var result = this.mapper.Map<ProductItemResultDto>(existProductItem);
        result.Variations = (await variationService.GetFeaturesOfProduct(categoryId, id)).ToList();

        return result;
    }

    public async Task<IEnumerable<ProductItemResultDto>> GetAllAsync()
    {
        var ProductItems = await this.repository.GetAll(includes: new[] { "Product", "ProductItemAttachments.Attachment" }).ToListAsync();

        return this.mapper.Map<IEnumerable<ProductItemResultDto>>(ProductItems);
    }

    public async Task<ProductItemResultDto> AddImageAsync(long productItemId, AttachmentCreationDto dto)
    {
        var existProductItem = await this.repository.GetAsync(p => p.Id.Equals(productItemId), 
            new string[] {"Product", "ProductItemAttachments" })
            ?? throw new NotFoundException($"This productId was not found with {productItemId}");

        var createdAttachment = await this.attachmentService.UploadImageAsync(dto);

        var mappedProduct = this.mapper.Map<ProductItemResultDto>(existProductItem);

        var productItemAttachment = new ProductItemAttachmentCreationDto()
        {
            ProductItemId = productItemId,
            AttachmentId = createdAttachment.Id,
        };

        mappedProduct.ProductItemAttachments.Add(await this.productItemAttachmentService.CreateAsync(productItemAttachment));
        return mappedProduct;
    }

    public async Task<bool> DeleteImageAsync(long productItemId, long imageId)
    {
        var existProductItem = await this.repository.GetAsync(p => p.Id.Equals(productItemId),
            new string[] {"Product", "ProductItemAttachments.Attachment" })
            ?? throw new NotFoundException($"This productId was not found with {productItemId}");

        await attachmentService.DeleteImageAsync(imageId);
        await productItemAttachmentService.DeleteAsync(productItemId, imageId);

        var image = existProductItem.ProductItemAttachments.FirstOrDefault(p=>p.AttachmentId.Equals(imageId));

        return true;
    }
}
