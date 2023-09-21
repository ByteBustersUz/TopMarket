using Domain.Entities.ProductFolder;

namespace Service.DTOs.ProductItems;

public class ProductItemCreationDto
{
    public string SKU { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }
    public long ProductId { get; set; }
}
