using AutoMapper;
using Data.IRepositories;
using Data.Repositories;
using Domain.Entities.AttachmentFolder;
using Domain.Entities.ProductFolder;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.ProductItemAttachments;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services;

public class ProductItemAttachmentService : IProductItemAttachmentService
{
    private readonly IMapper mapper;
    private readonly IRepository<ProductItem> productItemRepository;
    private readonly IRepository<ProductItemAttachment> repository;
    private readonly IRepository<Attachment> attachmentRepository;
    public ProductItemAttachmentService(
        IMapper mapper,
        IRepository<ProductItem> productItemRepository,
        IRepository<ProductItemAttachment> repository,
        IRepository<Attachment> attachmentRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.productItemRepository = productItemRepository;
        this.attachmentRepository = attachmentRepository;
    }

    public async Task<ProductItemAttachmentResultDto> CreateAsync(ProductItemAttachmentCreationDto dto)
    {
        var existProductItem = await this.productItemRepository.GetAsync(c => c.Id.Equals(dto.ProductItemId))
            ?? throw new NotFoundException($"This productItem was not found with {dto.ProductItemId}");

        var existAttachment = await this.attachmentRepository.GetAsync(c => c.Id.Equals(dto.AttachmentId))
            ?? throw new NotFoundException($"This attachment was not found with {dto.AttachmentId}");

        var mappedProductItemAttachment = this.mapper.Map<ProductItemAttachment>(dto);

        await this.repository.AddAsync(mappedProductItemAttachment);
        await this.repository.SaveAsync();

        return this.mapper.Map<ProductItemAttachmentResultDto>(mappedProductItemAttachment);
    }

    public async Task<ProductItemAttachmentResultDto> UpdateAsync(ProductItemAttachmentUpdateDto dto)
    {
        var existProductItemAttachment = await this.repository.GetAsync(c => c.Id.Equals(dto.Id))
            ?? throw new NotFoundException($"This productItemAttachment was not found with {dto.Id}");

        var existProductItem = await this.productItemRepository.GetAsync(c => c.Id.Equals(dto.ProductItemId))
            ?? throw new NotFoundException($"This productItem was not found with {dto.ProductItemId}");

        var existAttachment = await this.attachmentRepository.GetAsync(c => c.Id.Equals(dto.AttachmentId))
            ?? throw new NotFoundException($"This attachment was not found with {dto.AttachmentId}");

        var mappedProductItemAttachment = this.mapper.Map(dto, existProductItemAttachment);

        this.repository.Update(mappedProductItemAttachment);
        await this.repository.SaveAsync();

        return this.mapper.Map<ProductItemAttachmentResultDto>(mappedProductItemAttachment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existProductItemAttachment = await this.repository.GetAsync(c => c.Id.Equals(id))
            ?? throw new NotFoundException($"This productItemAttachment was not found with {id}");

        this.repository.Delete(existProductItemAttachment);
        await this.repository.SaveAsync();

        return true;
    }

    public async Task<ProductItemAttachmentResultDto> GetByIdAsync(long id)
    {
        var existProductItemAttachment = await this.repository.GetAsync(c => c.Id.Equals(id))
            ?? throw new NotFoundException($"This productItemAttachment was not found with {id}");

        return this.mapper.Map<ProductItemAttachmentResultDto>(existProductItemAttachment);
    }

    public async Task<IEnumerable<ProductItemAttachmentResultDto>> GetAllAsync()
    {
        var productItemAttachments = await this.repository.GetAll().ToListAsync();

        return this.mapper.Map<IEnumerable<ProductItemAttachmentResultDto>>(productItemAttachments);
    }
}
