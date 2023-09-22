using Service.DTOs.VariationOptions;

namespace Service.Interfaces;

public interface IVariationOptionService
{
    Task<VariationOptionResultDto> CreateAsync(VariationOptionCreationDto dto);
    Task<VariationOptionResultDto> UpdateAsync(VariationOptionUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<VariationOptionResultDto> GetByIdAsync(long id);
    Task<IEnumerable<VariationOptionResultDto>> GetAllAsync();
}
