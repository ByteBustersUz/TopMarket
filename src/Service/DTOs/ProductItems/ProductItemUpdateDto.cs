﻿namespace Service.DTOs.ProductItems;

public class ProductItemUpdateDto
{
    public long Id { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }
    public decimal QuantityInStock { get; set; }
    public long ProductId { get; set; }
}
