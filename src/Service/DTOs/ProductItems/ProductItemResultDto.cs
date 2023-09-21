using Service.DTOs.Products;

namespace Service.DTOs.ProductItems;

public class ProductItemResultDto { 
    public long Id { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }
    public ProductResultDto Product { get; set; }

    //public AttachmentResultDto? Attachment { get; set; }
}