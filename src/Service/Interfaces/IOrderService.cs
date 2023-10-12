﻿using Service.DTOs.Orders;

namespace Service.Interfaces;

public interface IOrderService
{
    Task<OrderResultDto> CreateAsync(OrderCreationDto dto);
    Task<OrderResultDto> UpdateAsync(OrderUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<OrderResultDto> GetByIdAsync(long id);
    Task<IEnumerable<OrderResultDto>> GetAllAsync();
}
