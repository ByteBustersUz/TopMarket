using Domain.Configuration;
using Service.DTOs.Countries;

namespace Service.Interfaces;

public interface ICountryService
{
    Task<bool> SetAsync();
    Task<CountryResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<CountryResultDto>> RetrieveAllAsync(PaginationParams @params);
}
