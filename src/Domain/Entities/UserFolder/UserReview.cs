using Domain.Commons;
using Domain.Entities.OrderFolder;

namespace Domain.Entities.UserFolder;

public class UserReview : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public long OrderLineId { get; set; }
    public OrderLine OrderLine { get; set; } = default!;

    public string RatingValue { get; set; } = string.Empty!;
    public string Comment { get; set; } = string.Empty;
}
