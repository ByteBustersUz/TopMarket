using Service.DTOs.Carts;

namespace Service.Interfaces;

public interface ICartService
{
    Task<CartResultDto> CreateAsync(long userId);
    Task AddItemToCartAsync(long cartId, long productItemId);
    Task<ICollection<CartItemResultDto>> RetrieveAllItemsAsync(long cartId);
    Task<bool> ClearCartAsync(long cartId);
}
