namespace Service.DTOs.Categories;

public class CategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryResultDto? ParentCategory { get; set; }
}