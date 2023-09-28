using AutoMapper;
using Data.IRepositories;
using Domain.Entities.ProductFolder;
using Domain.Entities.Shopping;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Carts;
using Service.Interfaces;

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

    public async ValueTask<IEnumerable<CartItemResultDto>> AddAsync(CartItemCreationDto dto)
    {
        var cart= await cartRepository.GetAsync(cart=>cart.UserId.Equals(dto.UserId));


        var result = new List<CartItemResultDto>();
        foreach (var item in dto.Details)
        {
            var cartItem = new ShoppingCartItem
            {
                CartId = cart.Id,
                Price = item.Price,
                ProductId = item.ProductItemId,
                Quantity = item.Quantity,
                Summ = (decimal)item.Quantity * item.Price
            };
            await this.repository.AddAsync(cartItem);
            result.Add(this.mapper.Map<CartItemResultDto>(cartItem));
        }
        await this.repository.SaveAsync();

        return result;
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

    public IEnumerable<CartItemResultDto> RetrieveAll(long? cartId = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<CartItemResultDto> RetrieveByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
