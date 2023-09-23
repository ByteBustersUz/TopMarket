using Service.DTOs.ProductItemAttachments;

namespace Service.Interfaces;

public interface IProductItemAttachmentService
{
    Task<ProductItemAttachmentResultDto> CreateAsync(ProductItemAttachmentCreationDto dto);
    Task<ProductItemAttachmentResultDto> UpdateAsync(ProductItemAttachmentUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ProductItemAttachmentResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ProductItemAttachmentResultDto>> GetAllAsync();
}
