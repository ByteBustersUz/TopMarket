using Domain.Configuration;
using Service.DTOs.Addresses;

namespace Service.Interfaces;

public interface IAddressService
{
    Task<AddressResultDto> CreateAsync(AddressCreationDto dto);
    Task<AddressResultDto> ModifyAsync(AddressUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<AddressResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<AddressResultDto>> RetrieveAllAsync();
    Task<IEnumerable<AddressResultDto>> RetrieveAllAsync(PaginationParams @params);
}
