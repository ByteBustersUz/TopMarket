using Domain.Configuration;
using Service.DTOs.Districts;

namespace Service.Interfaces;

public interface IDistrictService
{
    Task<bool> SetAsync();
    Task<DistrictResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<DistrictResultDto>> RetrieveAllAsync(PaginationParams @params);
}
