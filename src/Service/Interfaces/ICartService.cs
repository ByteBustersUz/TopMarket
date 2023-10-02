
using Service.DTOs.Carts;

namespace Service.Interfaces;

public interface ICartService
{
    ValueTask<CartResultDto> RetrieveByUserIdAsync(long userId);
    ValueTask AddItemAsync(long cartId, long productId);
    Task<ICollection<CartItemResultDto>> GetAllItemsAsync();
}
