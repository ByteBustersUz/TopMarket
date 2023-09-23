using Service.DTOs.ProductAttachments;
using Service.DTOs.Variations;

namespace Service.Interfaces;

public interface IProductAttachmentService
{
    Task<ProductAttachmentResultDto> CreateAsync(ProductAttachmentCreationDto dto);
    Task<ProductAttachmentResultDto> UpdateAsync(ProductAttachmentUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ProductAttachmentResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ProductAttachmentResultDto>> GetAllAsync();
    Task<bool> DeleteAsync(long productId, long attachmentId);
}
