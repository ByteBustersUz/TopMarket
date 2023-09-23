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
using Service.Interfaces;

namespace Service.Services;

public class ProductItemService : IProductItemService
{
    private readonly IMapper mapper;
    private readonly IRepository<ProductItem> repository;
    private readonly IRepository<Product> productRepository;
    private readonly IAttachmentService attachmentService;
    private readonly IRepository<Attachment> attachmentRepository;
    private readonly IProductItemAttachmentService productItemAttachmentService;
    public ProductItemService(
        IMapper mapper,
        IRepository<ProductItem> repository,
        IRepository<Product> productRepository,
        IAttachmentService attachmentService,
        IRepository<Attachment> attachmentRepository,
        IProductItemAttachmentService productItemAttachmentService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.productRepository = productRepository;
        this.attachmentService = attachmentService;
        this.attachmentRepository = attachmentRepository;
        this.productItemAttachmentService = productItemAttachmentService;
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
        var existProductItem = await this.repository.GetAsync(c => c.Id.Equals(dto.Id), includes: new[] { "Product","OrderLines", "ProductConfigurations", "ProductItemAttachments.Attachment", "ShoppingCartItems" })
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
        var existProductItem = await this.repository.GetAsync(c => c.Id.Equals(id), includes: new[] { "Product","OrderLines", "ProductConfigurations", "ProductItemAttachments.Attachment", "ShoppingCartItems" })
            ?? throw new NotFoundException($"This productItem was not found with {id}");

        return this.mapper.Map<ProductItemResultDto>(existProductItem);
    }

    public async Task<IEnumerable<ProductItemResultDto>> GetAllAsync()
    {
        var ProductItems = await this.repository.GetAll(includes: new[] { "Product","OrderLines", "ProductConfigurations", "ProductItemAttachments.Attachment", "ShoppingCartItems" }).ToListAsync();

        return this.mapper.Map<IEnumerable<ProductItemResultDto>>(ProductItems);
    }

    public async Task<ProductItemResultDto> AddImageAsync(long productItemId, AttachmentCreationDto dto)
    {
        var existProductItem = await this.repository.GetAsync(p => p.Id.Equals(productItemId),
            new string[] { "Product", "OrderLines", "ProductConfigurations", "ProductItemAttachments.Attachment", "ShoppingCartItems" })
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

    public async Task<bool> DeleteImageAsync(long imageId, long productItemId)
    {
        var existProductItem = await this.repository.GetAsync(p => p.Id.Equals(productItemId),
            new string[] { "Product", "OrderLines", "ProductConfigurations", "ProductItemAttachments.Attachment", "ShoppingCartItems" })
            ?? throw new NotFoundException($"This productId was not found with {productItemId}");

        await attachmentService.DeleteImageAsync(imageId);
        await productItemAttachmentService.DeleteAsync(productItemId, imageId);

        var image = existProductItem.ProductItemAttachments.FirstOrDefault(p=>p.AttachmentId.Equals(imageId));

        existProductItem.ProductItemAttachments.Remove(image);

        return true;
    }
}
