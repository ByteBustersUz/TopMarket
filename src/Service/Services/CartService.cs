using AutoMapper;
using Data.IRepositories;
using Domain.Entities.Shopping;
using Microsoft.AspNetCore.Http;
using Service.DTOs.Carts;
using Service.Exceptions;
using Service.Interfaces;
using System.Net.Http;

namespace Service.Services;

public class CartService : ICartService
{
    private readonly IRepository<ShoppingCart> _repository;
    private readonly IMapper _mapper;
    private readonly ICartItemService _cartItemService;

    public CartService(IRepository<ShoppingCart> repository, IMapper mapper, ICartItemService cartItemService)
    {
        _repository = repository;
        _mapper = mapper;
        _cartItemService = cartItemService;
    }

    public ValueTask AddItemAsync(long cartId, long productId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<CartItemResultDto>> GetAllItemsAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<CartResultDto> RetrieveByUserIdAsync(long userId)
    {
        throw new NotImplementedException();
    }
}
