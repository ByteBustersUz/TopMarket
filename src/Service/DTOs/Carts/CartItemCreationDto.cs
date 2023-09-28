namespace Service.DTOs.Carts;

public class CartItemCreationDto
{
    public long UserId { get; set; }
    public ICollection<CartItemDetail> Details { get; set; }
}
