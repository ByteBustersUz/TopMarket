using Domain.Entities.ProductFolder;

namespace Service.DTOs.ProductItems;

public class ProductItemCreationDto
{
    public decimal Price { get; set; }
    public long ProductId { get; set; }
}
