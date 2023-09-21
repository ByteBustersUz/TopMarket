using Service.DTOs.Categories;

namespace Service.Interfaces;

public interface ICategoryService
{
    Task<CategoryResultDto> CreateAsync(CategoryCreationDto dto);
    Task<CategoryResultDto> UpdateAsync(CategoryUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<CategoryResultDto> GetByIdAsync(long id);
    Task<IEnumerable<CategoryResultDto>> GetAllAsync();
}
