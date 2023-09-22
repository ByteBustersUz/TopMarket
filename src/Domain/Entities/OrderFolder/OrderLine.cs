using Domain.Commons;
using Domain.Entities.ProductFolder;
using Domain.Entities.UserFolder;

namespace Domain.Entities.OrderFolder;

public class OrderLine : Auditable
{
    public long ProductItemId { get; set; }
    public ProductItem ProductItem { get; set; } = default!;

    public long OrderId { get; set; }
    public Order Order { get; set; } = default!;

    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public ICollection<UserReview> UserReviews { get; set; }
}
