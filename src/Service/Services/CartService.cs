using AutoMapper;
using Data.IRepositories;
using Domain.Entities.Shopping;
using Service.DTOs.Carts;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Services;

public class CartService : ICartService
{
    private readonly IRepository<ShoppingCart> repository;
    private readonly IMapper mapper;

    public CartService(IRepository<ShoppingCart> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async ValueTask<CartResultDto> RetrieveByUserIdAsync(long userId)
    {
        var cart = await this.repository.GetAsync(cart => cart.UserId.Equals(userId));
        return this.mapper.Map<CartResultDto>(cart);
    }
}
