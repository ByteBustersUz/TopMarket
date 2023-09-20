namespace Service.DTOs.Categories;

public class CategoryCreationDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long? ParentId { get; set; }
}
