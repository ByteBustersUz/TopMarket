using Service.DTOs.Carts;

namespace Service.Interfaces;

public interface ICartService
{
    Task<CartResultDto> CreateAsync();
    Task<CartItemResultDto> UpdateItemQuantityAsync(CartItemUpdateDto dto);
    Task AddItemToCartAsync(long cartId, long productItemId);
    Task<ICollection<CartItemResultDto>> RetrieveAllItemsAsync(long cartId);
    Task<bool> ClearCartAsync(long cartId);
}
