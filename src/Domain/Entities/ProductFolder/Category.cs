﻿using Domain.Commons;

namespace Domain.Entities.ProductFolder;

public class Category : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long ParentId { get; set; }
}
