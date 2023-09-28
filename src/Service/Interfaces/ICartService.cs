
using Service.DTOs.Carts;

namespace Service.Interfaces;

public interface ICartService
{
    ValueTask<CartResultDto> RetrieveByUserIdAsync(long userId);
}
