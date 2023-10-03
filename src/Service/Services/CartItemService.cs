using AutoMapper;
using Data.IRepositories;
using Data.Repositories;
using Domain.Configuration;
using Domain.Entities.ProductFolder;
using Domain.Entities.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Service.DTOs.Carts;
using Service.DTOs.Products;
using Service.Exceptions;
using Service.Extensions;
using Service.Interfaces;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace Service.Services;

public class CartItemService : ICartItemService
{
    private readonly IMapper mapper;
    private readonly IRepository<ShoppingCart> cartRepository;
    private readonly IRepository<ShoppingCartItem> repository;

    public CartItemService(IRepository<ShoppingCartItem> repository, IMapper mapper, IRepository<ShoppingCart> cartRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.cartRepository = cartRepository;
    }

    public async ValueTask<CartItemResultDto> AddAsync(CartItemCreationDto dto)
    {
        var existedItem = await this.repository.GetAsync(i =>
                  i.CartId.Equals(dto.CartId) &&
                  i.ProductItemId.Equals(dto.ProductItemId));

        if (existedItem is not null)
        {
            existedItem.Quantity += dto.Quantity;
            existedItem.Summ += (decimal)dto.Quantity * dto.Price;
            await this.repository.SaveAsync();
            return this.mapper.Map<CartItemResultDto>(existedItem);
        }

        var mappedItem = this.mapper.Map<ShoppingCartItem>(dto);

        await this.repository.AddAsync(mappedItem);
        await this.repository.SaveAsync();

        return this.mapper.Map<CartItemResultDto>(mappedItem);
    }

    public ValueTask<CartItemResultDto> ModifyAsync(CartItemUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> RemoveAllAsync(long cartId)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> RemoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CartItemResultDto>> RetrieveAllAsync(long cartId)
    {
        string[] inclusion = { };

        IQueryable<ShoppingCartItem> query = this.repository.GetAll(i => i.CartId.Equals(cartId), includes: inclusion);

        var items = await query.ToListAsync();

        return this.mapper.Map<IEnumerable<CartItemResultDto>>(items);
    }

    public async ValueTask<CartItemResultDto> RetrieveAsync(Expression<Func<ShoppingCartItem, bool>> expression)
    {
        string[] inclusion = { "ProductItem" };

        var theItem = await this.repository.GetAsync(expression, inclusion)
            ?? throw new NotFoundException("Cart item with such properties is not found.");

        return this.mapper.Map<CartItemResultDto>(theItem);
    }

    public async ValueTask<CartItemResultDto> RetrieveAsync(long id)
    {
        string[] inclusion = { "ProductItem" };

        var theItem = await this.repository.GetAsync(id, inclusion)
            ?? throw new NotFoundException("Cart item with such properties is not found.");

        return this.mapper.Map<CartItemResultDto>(theItem);
    }
}
