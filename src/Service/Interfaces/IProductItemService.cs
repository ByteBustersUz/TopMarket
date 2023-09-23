using Service.DTOs.Attachments;
using Service.DTOs.ProductItems;
using Service.DTOs.Products;
using Service.DTOs.Variations;

namespace Service.Interfaces;

public interface IProductItemService
{
    Task<ProductItemResultDto> CreateAsync(ProductItemCreationDto dto);
    Task<ProductItemResultDto> UpdateAsync(ProductItemUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ProductItemResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ProductItemResultDto>> GetAllAsync();
    Task<ProductItemResultDto> AddImageAsync(long productItemId, AttachmentCreationDto dto);
    Task<bool> DeleteImageAsync(long imageId, long productItemId);
}
