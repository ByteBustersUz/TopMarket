using Domain.Configuration;
using Service.DTOs.Regions;

namespace Service.Interfaces;

public interface IRegionService
{
    Task<bool> SetAsync();
    Task<RegionResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<RegionResultDto>> RetrieveAllAsync(PaginationParams @params);
}
