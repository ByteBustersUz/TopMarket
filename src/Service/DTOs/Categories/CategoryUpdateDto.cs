﻿namespace Service.DTOs.Categories;

public class CategoryUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long? ParentId { get; set; }
}
