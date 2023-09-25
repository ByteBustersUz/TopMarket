namespace Service.DTOs.ShoppingCartItem;

public class ShoppingCartItemCreationDto
{
    public long CartId { get; set; }
    public long ProductItemId { get; set; }
    public long Quantity { get; set; }
}

