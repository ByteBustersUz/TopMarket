using Service.DTOs.Categories;
using Service.DTOs.Promotions;

namespace Service.Interfaces;

public interface IPromotionService
{
    Task<PromotionResultDto> CreateAsync(PromotionCreationDto dto);
    Task<PromotionResultDto> UpdateAsync(PromotionUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<PromotionResultDto> GetByIdAsync(long id);
    Task<IEnumerable<PromotionResultDto>> GetAllAsync();
}
