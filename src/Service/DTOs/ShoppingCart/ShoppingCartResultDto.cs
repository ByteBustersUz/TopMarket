namespace Service.DTOs.ShoppingCart;

public class ShoppingCartResultDto
{
    public long UserId { get; set; }
    public ICollection<ShoppingCartResultDto> Items { get; set; }
}
