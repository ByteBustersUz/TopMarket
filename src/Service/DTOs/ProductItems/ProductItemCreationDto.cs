using Domain.Entities.ProductFolder;

namespace Service.DTOs.ProductItems;

public class ProductItemCreationDto
{
    public decimal SKU { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }
    public long ProductId { get; set; }
}
