namespace Service.DTOs.ShoppingCartItem;

public class ShoppingCartItemResultDto
{
    public long Id { get; set; }
    public long CartId { get; set; }
    public long ProductItemId { get; set; }
    public long Quantity { get; set; }
}

