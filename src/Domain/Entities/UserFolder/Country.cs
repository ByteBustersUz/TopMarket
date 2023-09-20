using Domain.Commons;

namespace Domain.Entities.UserFolder;

public class Country : Auditable
{
    public string Name { get; set; } = string.Empty;
}
