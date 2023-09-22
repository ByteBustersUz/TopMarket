using Service.DTOs.Users;
using System.Globalization;

namespace Service.Interfaces;

public interface IAuthsService
{
    Task<UserResultDto> RegisterAsync(UserCreationDto dto);
    Task<UserResultDto> LoginAsync(UserLoginDto dto);
    Task<bool> ChangePasswordAsync(UserChangePassword dto);
    Task<UserResultDto> UpdateAsync(UserUpdateDto dto);
    Task<UserResultDto> GetByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> GetAllAsync();
}
