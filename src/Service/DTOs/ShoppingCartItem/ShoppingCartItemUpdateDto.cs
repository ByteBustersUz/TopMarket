namespace Service.DTOs.ShoppingCartItem;

public class ShoppingCartItemUpdateDto
{
    public long Id { get; set; }
    public long CartId { get; set; }
    public long ProductItemId { get; set; }
    public long Quantity { get; set; }
}

