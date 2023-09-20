namespace Domain.Commons;

public class Auditable
{
    public long Id { get; set; }
    public DateTime CretedAt { get; set; }
    public DateTime UpdatetAt { get; set; }
    public bool IsDeleted { get; set; }
}
