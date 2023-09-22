using Domain.Commons;
using Domain.Entities.UserFolder;

namespace Domain.Entities.Shopping;

public class ShoppingCart : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
}
